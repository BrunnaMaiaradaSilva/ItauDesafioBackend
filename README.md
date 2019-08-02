# Desafio backend do Itaú

Criado como solução do [Teste de backend do Itaú](https://github.com/cairano/backend-test)

## Requisitos
* .NET Core SDK 2.1

## Testes unitários
Os testes unitários podem ser executados a partir do arquivo *TestesUnitarios.cmd*

## Compilação
O projeto pode ser compilado através do arquivo *Compilar.cmd*

## Execução
O projeto *Aplicacao.Console* deve ser executado depois da compilação

## Como o projeto funciona
* No projeto *Core*, temos a classe *Movimentacao* que representa os dados de uma movimentação bancária (descrição, valor, data, etc). Nesse projeto estão todas as regras de negócio.

* Existe o conceito de "leitores de movimentação", que são classes que vão retornar uma lista de movimentações bancárias de algum lugar (da api, por exemplo).

* Há uma abstração para esse leitor, chamada *ILeitorDeMovimentacao*, que possui um método chamado *RetornarMovimentacoesAsync* que, como o nome sugere, retorna uma coleção de movimentações bancárias

* Existem 3 implementações desse leitor de movimentação:
### Api
Retorna movimentações bancárias através de uma api.

Precisa de um objeto do tipo *DadosDaApi* no construtor, que contém os dados da api (url base, endpoint de recebimentos e endpoint de pagamentos)

O método *RetornarMovimentacoesAsync* retorna os dados dos endpoints mencionados acima. O resultado é uma string que representa um array em JSON, que é passado para o leitor de Json (veja a baixo) e retornado logo em seguida. Foi feito assim pois a lógica de transformar um json em movimentação não é responsabilidade dessa classe.

### Json
Retorna movimentações bancárias através de uma string JSON. Essa string deve representar um array de movimentações bancárias.

### Log
Retorna movimentações bancárias através de uma string no formato de log.

No projeto *Aplicacao.Console*, nós lemos um arquivo chamado *logmovimentacao.txt*, que contém os dados de movimentação bancária no formado apresentado no [repositório do desafio](https://github.com/cairano/backend-test).

* No projeto *Aplicacao*, temos a classe *RelatorioMovimentacao* que representa os dados de um relatório de movimentações bancárias (quanto recebeu, qual categoria teve mais gastos, etc). Dentro dela também há um método estático chamado *Criar*, que recebe uma lista de movimentações e cria o relatório de fato. Não há como instanciar essa classe diretamente, somente pelo método Criar.

* Ainda nesse projeto, temos uma classe de serviço chamada *RelatorioMovimentacaoServico*, que recebe uma coleção de leitores de movimentação no construtor. Nessa classe, há um método chamado *RetornarRelatorioAsync*, que chama o método *RetornarMovimentacoesAsync* de cada *ILeitorDeMovimentacao* (passado no construtor), agrega tudo e retorna um relatório usando o método *Criar* da classe *RelatorioMovimentacao*. A idéia dessa classe é ser reusada em vários projetos diferentes (um Console, ASP.Net Core, WPF, etc)