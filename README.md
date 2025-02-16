# **Gestão Conta- Plataforma de Controle Financeiro Pessoal**

## **1. Apresentação**

Bem-vindo ao repositório do projeto **Gestão Conta**. Este projeto é uma entrega do MBA DevXpert Full Stack .NET e é referente ao módulo **Desenvolvimento Full-Stack Avançado com ASP.NET Core**.
Desenvolver uma aplicação full-stack que permita aos usuários gerenciar suas finanças pessoais através de uma API RESTful com front em angular.

### **Autor(es)**
- **Ozias Costa**
- **Victor Lino**
- **Jairo Bionez**
- **Darclê Fredrez**
- **Fernando Motta**

## **2. Proposta do Projeto**

O projeto consiste em:

- **API RESTful:** Exposição dos recursos da gestão de conta para integração com outras aplicações ou desenvolvimento de front-ends alternativos.
- **Autenticação e Autorização:** Implementação de controle de acesso, com a utlilização de claims para autorização.
- **Acesso a Dados:** Implementação de acesso ao banco de dados através de ORM.

## **3. Tecnologias Utilizadas**

- **Linguagem de Programação:** C#
- **Frameworks:**
  - ASP.NET Core Web API
  - Entity Framework Core
  - Angular
- **Banco de Dados:** SQLite
- **Autenticação e Autorização:**
  - ASP.NET Core Identity
  - JWT (JSON Web Token) para autenticação na API
- **Front-end:**   
  - HTML/CSS para estilização básica
  - typescript para controle de ações
- **Documentação da API:** Swagger

## **4. Estrutura do Projeto**

A estrutura do projeto é organizada da seguinte forma:

- front/ - projeto angular com NX 
- src/  
  - GestaoContas.Api/ - API RESTful
  - GestaoContas.Shared/ - Modelos de Dados e Configuração do EF Core
- README.md - Arquivo de Documentação do Projeto
- FEEDBACK.md - Arquivo para Consolidação dos Feedbacks
- .gitignore - Arquivo de Ignoração do Git

## **5. Funcionalidades Implementadas**

- **CRUD para Contas, Orçamentos e Transações:** Permite criar, editar, visualizar e excluir contas, orçamentos e transações.
- **Autenticação e Autorização:** Controle de acesso e autorização baseada em claims.
- **API RESTful:** Exposição de endpoints para operações via API.
- **Documentação da API:** Documentação automática dos endpoints da API utilizando Swagger.

## **6. Como Executar o Projeto**

### **Pré-requisitos**
- Node
- NX
- .NET SDK 8.0 ou superior
- SQlite
- Visual Studio 2022 ou superior (ou qualquer IDE de sua preferência)
- Git

### **Passos para Execução**

1. **Clone o Repositório:**
   - `git clone https://github.com/jairobionez/gestao-contas-mba.git`
   - `cd gestao-contas-mba`

2. **Configuração do Banco de Dados:**
   - No arquivo `sharedsettings.json`, localizado na pasta **CommonConfigurations** do projeto **GestaoContas.Shared**, configure a string de conexão do SQLite. Esta configuração é compartilhada por todos os projetos incluídos nesta solução.
   - Utilizando prompt de comando, acesse a pasta raiz do projeto **GestaoContas.Shared** e execute o comando **dotnet ef database update**
   - O seed configurará um usuário com perfil Admin. 
     - login: admin@teste.com
     - senha: Admin@123

3. **Executar a Aplicação MVC:**
   - `cd front`
   - `npm install -g nx`
   - `npm i --force`
   - `nx s gestao-compra`
   - Acesse a aplicação em: http://localhost:4200

4. **Executar a API:**
   - `cd src/GestaoContas.Api/`
   - `dotnet run`
   - Acesse a documentação da API em: http://localhost:5050/swagger

## **7. Instruções de Configuração**

- **JWT para API:** As chaves de configuração do JWT estão no `appsettings.json`.
- **Migrações do Banco de Dados:** As migrações são gerenciadas pelo Entity Framework Core. Não é necessário aplicar devido a configuração do Seed de dados.

## **8. Documentação da API**

A documentação da API está disponível através do Swagger. Após iniciar a API, acesse a documentação em:

http://localhost:5050/swagger

## **9. Avaliação**

- Este projeto é parte de um curso acadêmico e não aceita contribuições externas. 
- Para feedbacks ou dúvidas utilize o recurso de Issues
- O arquivo `FEEDBACK.md` é um resumo das avaliações do instrutor e deverá ser modificado apenas por ele.
