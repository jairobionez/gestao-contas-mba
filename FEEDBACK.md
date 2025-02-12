
# **Feedback 1a Entrega - Avaliação Geral e Recomendações**

## Front End

### Navegação

- Navegação simples, porém atende.
- O dashboard ficou bom com o uso dos graficos, mas poderia ter mais informações gerais

### Design

- Bom design, está simples, mas atende.

### Funcionalidade

- Talvez não seja ideal utilizar componentes mais defazados como o ng-brazil
- Adição interessante do NX
- Ao registrar um novo usuário a tela não redireciona para o dashboard, não ocorre nada.
- Muitas ações não funcionam (gestão de categorias, lançar despesas, etc)

## Back End

### Arquitetura

- A camada shared poderia ser dividida entre Data e Business, mas a Shared não é um grande problema.
- A arquitetura está bem simples, usando contexto, regras e fluxos de negócios direto na controller.

### Funcionalidade

- O processo de cadastro de conta poderia ter uma mensagem clara no caso de registro de um e-mail já existente
- Não encontrei  a funcionalidade de gestão de budget (limites de gastos)
- Muitas ações não funcionam (gestão de categorias, lançar despesas, etc)

### Modelagem

- Classes de dados super simples, apenas para mapeamento do banco
- Não vi validação de dados de entrada nem pela entidade, nem pela ViewModel

## Projeto

### Organização

- O diretorio poderia agrupar ambos os projetos em SRC e dividir nas sub-pastas

### Documentação

- Bem documentado, tanto no projeto, API quanto no repositório no GH.

### Instalação

- Instalação ideal, bastou rodar os comandos especificados no repositório.