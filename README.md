# Desafio Backend Totvs 2020

**Parabéns!** <br>
Ficamos muito felizes por você ter chegado até aqui e agora chegou a hora de botar as mãos no código e mostrar toda a sua habilidade.
Nessa etapa queremos ver como você pensa e estrutura seu código, por isso se liga no que vamos desenvolver.


## O desafio
Nosso desafio contempla em resolver a seguinte situação do cotidiano dos operadores de pontos de venda (PDV): <br><br>
Esses profissionais tem grande responsabilidade em suas mãos e a maior parte do seu tempo é gasto recebendo valores de clientes e, em alguns casos, fornecendo troco. <br> <br>
Seu desafio é criar uma API que leia o valor total a ser pago e o valor efetivamente pago pelo cliente, em seguida informe o menor número de cédulas e moedas que devem ser fornecidas como troco.

## Regras
* Notas disponíveis: **R$ 10,00 - R$ 20,00 - R$ 50,00 - R$ 100,00**
* Moedas disponíveis: **R$0,01 - R$0,05 - R$0,10 - R$0,50**
* Entregar o menor número de notas possíveis

Exemplo:<br><br>
```Valor do Troco: R$ 30,00```<br>
```Resultado Esperado: Entregar 1 nota de R$20,00 e 1 nota de R$10,00```<br><br>
```Valor do Troco: R$ 80,00```<br>
```Resultado Esperado: Entregar 1 nota de R$50,00, 1 nota de R$20,00 e 1 nota de R$10,00```<br>

## O que será avaliado
* Estrutura e organização do código
* Qualidade do código
* Boas práticas e padrões utilizados

## Requisitos obrigatórios
* .Net Core
* A aplicação deve conectar-se em um banco de dados utilizando Entity Core (SQL Server ou Postgree)
* As consultas devem ser realizadas utilizando o ORM Dapper
* As transações de troco (saída de caixa) devem ficar registradas
* A aplicação deve estar coberta com testes unitários
* Swagger para documentação das rotas

## Requisitos desejáveis
* Docker
* Transações em um banco não relacional

## Dicas
* Tente não fazer tudo em um mesmo commit.
* Assim que terminar, mande um email avisando e não faça mais commits depois disso.

<br>
<br>

**Boa sorte! Estamos anciosos pra ter você na equipe.**


