# Desafio backend do Itaú

Criado como solução do ([Teste de backend do Itaú](https://github.com/cairano/backend-test))

## Requisitos
* .NET Core SDK 2.1

## Testes unitários
Os testes unitários podem ser executados a partir do arquivo *TestesUnitarios.cmd*

## Compilação
O projeto pode ser compilado através do arquivo *Compilar.cmd*

## Como o projeto funciona
* No projeto *Core*, temos a classe *Movimentacao* que representa os dados de uma movimentação bancária (descrição, valor, data, etc).

* Existem o conceito de "leitores de movimentação", que são classes que vão retornar uma lista de movimentações bancárias de algum lugar (da api, por exemplo).

* Há uma abstração para esse leitor, chamada *ILeitorDeMovimentacao*, que possui um método chamado *RetornarMovimentacoesAsync* que, como o nome sugere, retorna uma coleção de movimentações bancárias

* Existem 3 implementações desse leitor de movimentação:
### Api
Retorna movimentações bancárias através de uma api

### Json
Retorna movimentações bancárias através de uma string JSON

### Log
Retorna movimentações bancárias através de uma string no formato de log

* No projeto *Aplicacao*, temos a classe *RelatorioMovimentacao* que representa os dados de um relatório de movimentações bancárias (quanto recebeu, qual categoria teve mais gastos, etc). Dentro dela também há um método estático chamado *Criar*, que recebe uma lista de movimentações e cria o relatório de fato. Não há como instanciar essa classe diretamente, somente pelo método Criar.

* Ainda nesse projeto, temos uma classe de serviço chamada *RelatorioMovimentacaoServico*, que recebe uma coleção de leitores de movimentação no construtor. Nessa classe, há um método chamado *RetornarRelatorioAsync*, que chama o método *RetornarMovimentacoesAsync* de cada *ILeitorDeMovimentacao* (passado no construtor), agrega tudo e retorna um relatório usando o método *Criar* da classe *RelatorioMovimentacao*. A idéia dessa classe é ser reusada em vários projetos diferentes (um Console, ASP.Net Core, WPF, etc)