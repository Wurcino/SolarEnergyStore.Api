Descrição do Projeto

O SolarEnergyStore.Api é uma API que gerencia equipamentos elétricos em uma empresa utilizando dados de sensores conectados a um Arduino. A API permite:

Registro de temperaturas internas e externas dos equipamentos.
Monitoramento e controle dos períodos de funcionamento.
Integração com um banco de dados relacional para persistência de dados.
Tecnologias Utilizadas
ASP.NET  Core 8.0: Framework para construção de APIs modernas.
Entity Framework Core: ORM para interagir com o banco de dados.
SQL Server: Banco de dados relacional.
Arduino: Coleta de dados de sensores (temperatura e proximidade).
Swagger: Documentação e testes interativos da API.
Estrutura do Projeto
A solução está dividida em múltiplos projetos para separar responsabilidades e melhorar a manutenção:

SolarEnergyStore.Api:
Contém os controllers que expõem os endpoints da API.
Configuração de dependências e inicialização da aplicação.
SolarEnergyStore.Models:
Contém as classes de modelo (entidades) que representam as tabelas do banco de dados.
SolarEnergyStore.Repositories:
Camada de acesso ao banco de dados utilizando o Entity Framework Core.
Contém a implementação do DbContext.
SolarEnergyStore.Services:
Contém a lógica de negócios e validações.
SolarEnergyStore.Tests:
Testes unitários para garantir a qualidade do código.
Pré-requisitos
Antes de começar, você precisa instalar as seguintes ferramentas:

.NET SDK  (versão 8.0 ou superior)
SQL Server 
Arduino IDE  (para o código do Arduino)
Um cliente de API, como Postman  ou Insomnia , para testes manuais.
Endpoints Disponíveis
Os principais endpoints estão documentados via Swagger. Após iniciar a aplicação, acesse:


https://localhost:5001/swagger/index.html
Você verá uma interface interativa com a documentação completa da API e poderá realizar testes diretamente por lá.

Testes
Para executar os testes unitários, use o seguinte comando no terminal:


cd SolarEnergyStore.Tests
dotnet test
O resultado dos testes será exibido no terminal, incluindo detalhes de qualquer falha.

Como Funciona a Integração com o Arduino
O Arduino coleta dados de sensores e envia para a API por meio de requisições HTTP. Aqui está uma visão geral:

Sensores:

Proximidade: Detecta a presença de pessoas para ligar ou desligar os equipamentos.
Temperatura: Monitora a temperatura interna e externa dos equipamentos.
Fluxo:

O Arduino envia os dados para os endpoints da API utilizando comandos HTTP (via biblioteca Arduino HTTPClient).
A API processa os dados, armazena no banco de dados e retorna as respostas adequadas ao Arduino.
