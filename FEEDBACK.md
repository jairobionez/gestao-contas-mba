
# **Feedback 2a Entrega - Avaliação Geral**

## Front End

### Navegação

- Navegação simples, porém atende.
- O dashboard ficou bom com o uso dos graficos e informações gerais

### Design

- Bom design, está simples, mas atende.

### Funcionalidade

- Demais funcionalidades operando corretamente com exceções:
    - Talvez não seja ideal utilizar componentes mais defazados como o ng-brazil
    - Não deveriam forçar a descrição da categoria possuir no minimo 10 caracteres
    - As categorias são listadas como inativas (não entendi o motivo)
    - Deveria ser possível usar um orçamento geral no lugar das categorias
    - Ao exceder um limite de categoria aparece um alerta de erro, mas é um aviso, ou seja, houve sucesso no registro (mas gera duvidas)
    - Não encontrei a feature de relatórios
    - Não é possível informar valores no formato pt-BR (ex: 500,50)
    - Alertas de erro exibem erros não tratados (ex, valor numérico em formato inválido)

## Back End

### Arquitetura

- Em geral a arquitetura está coesa, bem divida em camadas responsáveis.
- Observei o uso de diversas boas práticas utilizadas nos cursos
- Não vi sentido para uma camada Shared com uma única classe, utilizada em um único lugar de outro projeto, isso é uma violação do YAGNI/KISS
- O projeto possui bastante "Dead Code" uma má prática prevista no Clean Code
- Não vi necessidade de 2 versões de controllers V1 e V2, não é um projeto de produção.
- Evitar usar Data Annotations em Modelos de negócio
- Seria interessante levar a manipulação de dados (ex: dashboard) para dentro de um repositório

### Funcionalidade

- No geral as funcionalidades expostas na API funcionam bem

### Modelagem

- Modelagem simples, porém de acordo com a complexidade do projeto.

## Projeto

### Organização

- O arquivo de solução deveria estar na pasta SRC
- O diretorio poderia agrupar ambos os projetos em SRC e dividir nas sub-pastas (o ideal é uma pasta Back-End e outra Front-End)

### Documentação

- Bem documentado, tanto no projeto, API quanto no repositório no GH.

### Instalação

- Ao seguir os passos informados na documentação foi possível rodar o projeto