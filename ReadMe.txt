O projeto foi criado utilizando Clean Arquitecture, segue o resumo das etapas:
Primeiramente foi analisado a relações entre as entidades, para criar a camada Core. Utilizei alguns fundamentos
do DDD para aplicar a regra de negócio da própria entidade.
Logo após comecei a implementar a interface de ApplicationUser, para criar as camadas de Serviço, Repositório
e a Controller, tudo isso usando injeção de dependência.
Quando a primeira entidade já estava feita, criei o DbContext, para fazer a primeira migrations, e já começar
a testar os primeiros endpoints e também para testar as autenticações.
Logo após foi criado entidade por entidade, criando suas regras de negócio na Core, algumas regras na camada
de serviço e toda a manipulação de dados na camada Repositório.
No final foram feitos alguns testes para validar as regras.

Obs: No final foi deletada todas  Migrations anteriores, para quem for testar o projeto possa criar o banco de dados.
Foi criado uma Migration "NewDataBase"(camada de Infrastructure) para que o usuário faça o "Update-Database".

Vou deixar um comando SQL também, para a pessoa criar por fora também. 

