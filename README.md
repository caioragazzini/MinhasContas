
# Projeto-LG
Sistema de gerenciamento de Fatura, gerencia os cadastros de Faturas, Emissor da Fatura e sua Categoria.

## Descrição do Projeto
<p align="left">
	Projeto
	<p>- Projeto desenvolvido em Asp.NET Core MVC, utilizando a linguagem C# e Bancos relacionais SQL Server.</p>
	
### Features
Models
- [x] Categoria
- [x] CategoriaModel
- [x] Emissor
- [x] EmissorModel
- [x] Fatura
- [x] FaturaModel 

Controller
- [x] CategiraController
- [x] EmissorController
- [x] FaturaController

View
Categoria
- [x] Create
- [x] Delete
- [x] Details
- [x] Edit
- [x] Index

Emissor
- [x] Create
- [x] Delete
- [x] Details
- [x] Edit
- [x] Index

Fatura
- [x] Create
- [x] Delete
- [x] Details
- [x] Edit
- [x] Index

Relatórios
- [x] Index

Banco de Dados

CREATE TABLE [dbo].[Categoria] (
    [CategoriaId] INT           IDENTITY (1, 1) NOT NULL,
    [Nome]        VARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([CategoriaId] ASC)
);

CREATE TABLE [dbo].[Emissor] (
    [EmissorId]   INT           IDENTITY (1, 1) NOT NULL,
    [Nome]        VARCHAR (255) NULL,
    [CategoriaId] INT           NULL,
    PRIMARY KEY CLUSTERED ([EmissorId] ASC),
    FOREIGN KEY ([CategoriaId]) REFERENCES [dbo].[Categoria] ([CategoriaId])
);

CREATE TABLE [dbo].[Fatura] (
    [FaturaId]       INT          IDENTITY (1, 1) NOT NULL,
    [EmissorId]      INT          NULL,
    [ValorConta]     DECIMAL (18) NULL,
    [DataFatura]     DATETIME     NULL,
    [DataVencimento] DATETIME     NULL,
    [Status]         BIT          NULL,
    PRIMARY KEY CLUSTERED ([FaturaId] ASC),
    FOREIGN KEY ([EmissorId]) REFERENCES [dbo].[Emissor] ([EmissorId])
);



#### Pré-requisitos

Antes de começar, você vai precisar ter instalado em sua máquina as seguintes ferramentas:
- Visual Studio 2019

##### 🎲 Rodando o Projeto Minhas Contas (servidor)

```bash
# Clone este repositório
$ git clone <https://github.com/caioragazzini/MinhasContas.git>

# Acesse a pasta do projeto no terminal/cmd
$ cd nlw1

# Vá para a pasta server
$ cd server

# Instale as dependências
$ npm install

# Execute a aplicação em modo de desenvolvimento
$ npm run dev:server

###### 🛠 Tecnologias

As seguintes ferramentas foram usadas na construção do projeto:

- [Microsoft Visual Studio](https://expo.io/)
- .Net Framework 4.7.2
- Asp.NET Core MVC
- Sql Server


Autor
Caio Ragazzini
(92) 98835-9687
