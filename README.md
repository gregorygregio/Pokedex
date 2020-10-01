# Instruções para o teste




## Fork e pull-request
Siga as instruções [deste artigo](https://blog.da2k.com.br/2015/02/04/git-e-github-do-clone-ao-pull-request/) para fazer um fork neste repositório, cloná-lo, fazer o teste em uma branch com o nome do candidato e depois fazer uma pull-request.


## Executando as migrations:

Clonado o repositório, execute o MigrationsRunner, passando como argumento a string de conexão:


```bash
"User ID=<user>;Password=<pwd>;Host=localhost;Port=<port>;Database=<db>;Pooling=true;"
```

As tabelas necessárias para o teste serão criadas:
* pokemon,
* types,
* pokemon_types;

A tabela principal é a _pokemon_, com a seguinte estrutura:

* **id**  -> varchar(36) Aqui vai um Guid (com os hífens)
* **pokedex_index** -> um inteiro único, correspondente ao index do pokedex do pokemon 
* **name** -> varchar(26) ÚNICO com o nome do pokemon
* **hp** -> inteiro correspondente à HP, com valor entre 1 e 255 (inclusos)
* **attack** -> inteiro correspondente aos pontos de ataque, valor entre 5 e 190 (inclusos)
* **defense**  -> inteiro correspondente aos pontos de defesa, valor entre 5 e 230 (inclusos)
* **special_attack** -> inteiro correspondente aos pontos de sp. ataque, valor entre 10 e 194 (inclusos)
* **special_defense** -> inteiro correspondente aos pontos de sp. defesa, valor entre 20 e 230 (inclusos)
* **speed** -> inteiro correspondente aos pontos de velocidade, valor entre 5 e 180 (inclusos)
* **generation** -> inteiro correspondente à geração do pokemon, valor entre 1 e 6 (inclusos)

**Nenhum dos campos pode ser nulo**

A tabela types contém os possíveis tipos de pokemon, sua estrutura é:

* **id**  -> varchar(36) Aqui vai um Guid (com os hífens);
* **type** -> um varchar(36) contendo a descrição do tipo;

E a tabela que relaciona as duas anteriores, pokemon_types:


* **id**  -> varchar(36) Aqui vai um Guid (com os hífens);
* **pokemon_id** -> um varchar(36) com um id de um pokemon;
* **type_id** -> um varchar(36) com um id de um tipo;

A migration também seedará os valores da tabela _types_, além de inserir o primeiro pokemon de exemplo;