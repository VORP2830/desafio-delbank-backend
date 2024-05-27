using System.Text.Json;
using AutoMapper;
using LocadoraDVD.Application.Dtos;
using LocadoraDVD.Application.Interfaces;
using LocadoraDVD.Domain.Entities;
using LocadoraDVD.Domain.Exceptions;
using LocadoraDVD.Domain.Interfaces;
using LocadoraDVD.Domain.Interfaces.Mongo;

namespace LocadoraDVD.Application.Services;
public class DirectorService : IDirectorService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMessageBusService _messageBus;
    private readonly ICache _cache;
    private readonly IUnitOfWorkMongo _unitOfWorkMongo;
    public DirectorService(IMapper mapper, IUnitOfWork unitOfWork, IMessageBusService messageBus, ICache cache, IUnitOfWorkMongo unitOfWorkMongo)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _messageBus = messageBus;
        _cache = cache;
        _unitOfWorkMongo = unitOfWorkMongo;
    }

    public async Task<IEnumerable<DirectorDto>> GetAll()
    {
        IEnumerable<Director> directors = await _unitOfWorkMongo.DirectorMongoRepository.GetAll();
        return _mapper.Map<IEnumerable<DirectorDto>>(directors);
    }

    public async Task<DirectorDto> GetById(int id)
    {
        Director director = await _cache.Get<Director>(id.ToString());
        if (director is null)
        {
            director = await _unitOfWorkMongo.DirectorMongoRepository.GetById(id);
            if (director is null) return null;
            await _cache.Store(director.Id.ToString(), director);
        }
        return _mapper.Map<DirectorDto>(director);
    }

    public async Task<DirectorDto> Create(DirectorDto model)
    {
        Director director = _mapper.Map<Director>(model);
        _unitOfWork.DirectorRepository.Add(director);
        if (await _unitOfWork.SaveChangesAsync())
        {
            _messageBus.Publish(director, "director");
            await _cache.Store<Director>(director.Id.ToString(), director);
            return _mapper.Map<DirectorDto>(director);
        }
        throw new LocadoraDVDException("Falha ao criar diretor");
    }

    public async Task<DirectorDto> Update(DirectorDto model)
    {
        Director director = await _unitOfWork.DirectorRepository.GetById(model.Id);
        if (director is null)
        {
            throw new LocadoraDVDException("Diretor não encontrado");
        }
        _mapper.Map(model, director);
        _unitOfWork.DirectorRepository.Update(director);
        if (await _unitOfWork.SaveChangesAsync())
        {
            _messageBus.Publish(director, "director");
            await _cache.Store<Director>(director.Id.ToString(), director);
            return _mapper.Map<DirectorDto>(director);
        }
        throw new LocadoraDVDException("Falha ao alterar diretor");
    }

    public async Task<string> Delete(int id)
    {
        Director director = await _unitOfWork.DirectorRepository.GetById(id);
        if (director is null)
        {
            throw new LocadoraDVDException("Diretor não encontrado");
        }
        director.Delete();
        _unitOfWork.DirectorRepository.Update(director);
        if (await _unitOfWork.SaveChangesAsync())
        {
            _messageBus.Publish(director, "director");
            await _cache.Delete<Director>(director.Id.ToString());
            return "Diretor deletado com sucesso";
        }
        throw new LocadoraDVDException("Falha ao deletar diretor");
    }
}
