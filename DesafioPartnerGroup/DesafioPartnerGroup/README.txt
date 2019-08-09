Ivan Casa
Última modificação: 2019-08-09
Revisão: 1.0
Descrição: Solução para o gerenciamento de patrimônios de uma empresa.


******************************
* Visão geral
******************************
A solução está dividida em três projetos:
    - DesafioPartnerGroup
        * Um projeto Web contendo Web API REST expondo funcionalidades para o gerenciamento 
        * de patrimônios e de suas marcas (a marca de cada patrimônio).


    - Ivan.SQL
        * Biblioteca contendo as funcionalidades para acesso ao banco de dados e algumas validações.
        * Ela possui três divisões / espaços de nome / pastas:
        *     - "Models" contém as classes POCO (Plain Old CLR Objects) e dentro de cada classe
        *       atalhos / funções estáticas para a manipulação dos dados mas não a sua implementação!
        * 
        *     - "Services" contém a implementação das funções estáticas para a manipulação dos dados
        *       esta separação ajuda a alterar a tecnologia / maneira de acessar os dados sem alterar
        *       as classes principais POCO.
        * 
        *     - "IServices" contém as interfaces que definem as funções de acesso ao banco de dados
        *       possibilita usar diferentes servicos "Services" para o mesmo POCO / classe da aplicação


    - Ivan.Core
        * Biblioteca que prove fabrica de objetos e / ou injeção de referência para os POCOs.
        * estou utilizando apenas fabrica de objetos simples para criar minhas instâncias.


    - Ivan.Business.Rule
        * Biblioteca destinada a conter as regras de negócios.
        * por enquanto contém apenas uma exceção "NomeDaMarcaRepetidaException"



******************************
* Web API REST
******************************
O projeto Web "DesafioPartnerGroup" contendo Web API REST expõe funcionalidades para o gerenciamento 
de patrimônios e de suas marcas (a marca de cada patrimônio) nos seguintes controles:
    - MarcasController
        * GET: api/marcas
        *         Obtém todas as marcas disponíveis no banco de dados.
        *         Todos os objetos do tipo "Marca"
        *
        *
        * GET: api/marcas/{id}
        *         Obtém uma marca por ID.
        *         <parâmetro: "id"> ID da marca a ser recuperada.
        *         <retorna> Um objeto do tipo "Marca"
        *
        *
        * GET: api/marcas/{id}/{patrimonio}
        *         Obtém todos os patrimônios de uma marcas disponíveis no banco de dados.
        *         <parâmetro: "id"> ID do patrimônio a ser alterado.
        *         <parâmetro: "patrimonio"> ID do patrimônio a ser alterado.
        *         <retorna>  Todos os objetos do tipo "Marca" disponíveis no banco de dados.
        *
        *
        * POST: api/marcas
        *         Insere um novo objeto do tipo "Marca" no banco de dados caso as regras de negócio sejam respeitadas.
        *         Devido às regras de negócio caso uma marca com o nome repetido tente ser cadastrada uma excessão será lançada.
        *         <parâmetro: "entity">  Objeto do tipo "Marca" a ser inserido no banco de dados.
        *
        *
        * PUT: api/marcas/{id}
        *         Altera um objeto do tipo "Marca" no banco de dados caso as regras de negócio sejam respeitadas.
        *         Devido às regras de negócio não podem existir marcas com nomes repetidos
        *         Caso tente alterar para uma marca com o mesmo nome uma excessão será lançada.
        *
        *
        * DELETE : api/marcas
        *         Remove um objeto do tipo "Marca" do banco de dados caso 
        *
        *
    - PatrimoniosController
        * GET: api/patrimonios
        *         Obtém todos os patrimônios disponíveis no banco de dados.
        *         Todos os objetos do tipo "Patrimonio"
        *
        *
        * GET: api/patrimonios/{id}
        *         Obtém o patrimônio com o ID passado via parametro se disponível no banco de dados.
        *         <parâmetro: "id">   ID do Objeto do tipo "Patrimonio" desejado.
        *
        *
        * POST: api/patrimonios
        *         Insere um novo patrimônio no banco de dados.
        *
        *
        * PUT: api/patrimonio/{id}
        *         Altera os dados de um patrimônio no banco de dados.
        *         <parâmetro: "id"> ID do patrimônio a ser alterado.
        *
        *
        * DELETE: api/patrimonios/{id}
        *         Excluir um objeto do tipo "Patrimonio" do banco de dados.
        *         <parâmetro: "id"> ID do patrimônio a ser removido.



******************************
* Banco de dados
******************************
Para acesso aos dados existe uma com um projeto de banco de dados.
A solução e o projeto possuem o nome de "SQLDatabase"
    - SQLDatabase
        * Contém todos os scripts e recursos necessarios para criar o banco de dados e suas tabelas

