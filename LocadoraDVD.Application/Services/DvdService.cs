using AutoMapper;
using LocadoraDVD.Application.Dtos;
using LocadoraDVD.Application.Interfaces;
using LocadoraDVD.Domain.Entities;
using LocadoraDVD.Domain.Exceptions;
using LocadoraDVD.Domain.Interfaces;
using LocadoraDVD.Domain.Interfaces.Mongo;

namespace LocadoraDVD.Application.Services;
public class DvdService : IDvdService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMessageBusService _messageBus;
    private readonly ICache _cache;
    private readonly IUnitOfWorkMongo _unitOfWorkMongo;
    public DvdService(IMapper mapper, IUnitOfWork unitOfWork, IMessageBusService messageBus, ICache cache, IUnitOfWorkMongo unitOfWorkMongo)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _messageBus = messageBus;
        _cache = cache;
        _unitOfWorkMongo = unitOfWorkMongo;
    }
    public async Task<IEnumerable<DvdDto>> GetAll()
    {
        IEnumerable<Dvd> dvds = await _unitOfWorkMongo.DvdMongoRepository.GetAll();
        return _mapper.Map<IEnumerable<DvdDto>>(dvds);
    }

    public async Task<DvdDto> GetById(int id)
    {
        Dvd dvd = await _cache.Get<Dvd>(id.ToString());
        if (dvd is null)
        {
            dvd = await _unitOfWorkMongo.DvdMongoRepository.GetById(id);
            if (dvd is null) return null;
            await _cache.Store(id.ToString(), dvd);
        }
        return _mapper.Map<DvdDto>(dvd);
    }

    public async Task<DvdDto> Create(DvdDto model)
    {
        Director director = await _unitOfWorkMongo.DirectorMongoRepository.GetById(model.DirectorId);
        if (director is null)
        {
            throw new LocadoraDVDException("Diretor não encontrado");
        }
        Dvd dvd = _mapper.Map<Dvd>(model);
        _unitOfWork.DvdRepository.Add(dvd);
        if (await _unitOfWork.SaveChangesAsync())
        {
            _messageBus.Publish(dvd, "dvd");
            await _cache.Store<Dvd>(dvd.Id.ToString(), dvd);
            return _mapper.Map<DvdDto>(dvd);
        }
        throw new LocadoraDVDException("Falha ao criar DVD");
    }

    public async Task<DvdDto> Update(DvdDto model)
    {
        Director director = await _unitOfWorkMongo.DirectorMongoRepository.GetById(model.DirectorId);
        if (director is null)
        {
            throw new LocadoraDVDException("Diretor não encontrado");
        }
        Dvd dvd = await _unitOfWork.DvdRepository.GetById(model.Id);
        if (dvd is null)
        {
            throw new LocadoraDVDException("DVD não encontrado");
        }
        _mapper.Map(model, dvd);
        _unitOfWork.DvdRepository.Update(dvd);
        if (await _unitOfWork.SaveChangesAsync())
        {
            _messageBus.Publish(dvd, "dvd");
            await _cache.Delete<Dvd>(dvd.Id.ToString());
            return _mapper.Map<DvdDto>(dvd);
        }
        throw new LocadoraDVDException("Falha ao criar DVD");
    }

    public async Task<string> Delete(int id)
    {
        Dvd dvd = await _unitOfWork.DvdRepository.GetById(id);
        if (dvd is null)
        {
            throw new LocadoraDVDException("DVD não encontrado");
        }
        dvd.Delete();
        _unitOfWork.DvdRepository.Update(dvd);
        if (await _unitOfWork.SaveChangesAsync())
        {
            _messageBus.Publish(dvd, "dvd");
            return "DVD deletado com sucesso";
        }
        throw new LocadoraDVDException("Falha ao deletar DVD");
    }

}
