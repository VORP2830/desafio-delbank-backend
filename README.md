# README: LocadoraDVD

Este repositório contém os arquivos necessários para configurar e executar a aplicação LocadoraDVD em um ambiente Dockerizado.

## Estrutura do Projeto

- `./`: Contém o código-fonte da aplicação backend.
- `docker-compose.yml`: Arquivo de configuração do Docker Compose para orquestrar os contêineres da aplicação e dos serviços necessários.

## Pré-requisitos

- Docker
- Docker Compose

## Instruções de Uso

1. Clone este repositório para o seu ambiente local:

```bash
git clone https://github.com/VORP2830/desafio-delbank-backend.git
cd desafio-delbank-backend
```

2. Execute o seguinte comando para iniciar os serviços de infraestrutura (RabbitMQ, Redis, MongoDB, MySQL):

```bash
docker-compose up -d --build rabbitmq redis mongodb mysql
```

Este comando iniciará os contêineres para os seguintes serviços:
- RabbitMQ
- Redis
- MongoDB
- MySQL

3. Após garantir que todos os serviços estão em execução corretamente, execute o seguinte comando para iniciar a aplicação backend:

```bash
docker-compose up -d --build backend
```

A aplicação backend será iniciada e estará acessível em `http://localhost:80/swagger/index.html`.

## Observações

- Certifique-se de que as portas necessárias (no caso, a porta 80 para a aplicação backend) não estão sendo utilizadas por outros serviços em sua máquina.
---
