
# Biblioteca Exkyn Core

Uma biblioteca construída em .NET para uso geral dentro de projetos.

## O projeto é dividido em tres áreas

- Extensions

- Helpers

- Clients

## Extensions

#### ConvertExtension

Uma extensão das possibilidades de conversão entre elas um objeto em string ou uma lista em data table.

#### ExpirationDayExtension

Adiciona dias a data de forma corrida ou levando em consideração os dias uteis.

#### HourExtension

Permite adicionar a data a hora inicial ou final de um dia.

#### MoneyExtension

Formata um valor monetário adicionando a moeda desejada.

#### NoFormattingExtension

Remove caracteres especiais de uma string.

#### UpperFirstLetterExtension

Deixa a primeira letra em maiúscula

#### ValidateExtension

Permite validar CPF, CNPJ, PIS e E-mail.

## Helpers

#### DirectoryHelper

Permite criar diretórios e listar os arquivos existente no diretório.

#### EncryptHelper

Permite criptografar usando o SHA-256 ou o AES. Além de permitir descriptografar o AES.

#### FileHelper

Cria arquivos e permite escrever neles.

#### LogHelper

Permite adicionar logs em um arquivo.

## Clients

#### ApiClient

Permite consumir API externas configuradas para o JSON formato de API. Possui métodos GET e POST que retornam o objeto solicitado devidamente preenchido

## Instalação

Você pode instalar esse projeto pelo Nuget

```bash
  dotnet add package Exkyn.Core --version 8.0.0
```

## Autor

- [@Daniel Moura](https://github.com/dmodesigner/)

## Licença

Esse projeto é oferecido sobre uso da licença MIT. Sendo livre seu uso pessoal ou comercial.

Sendo oferecido sem garantias e de sua total responsabilidade seu uso.

Para maiores detalhes consulte o arquivo de [licença](https://github.com/dmodesigner/Exkyn.Core/blob/master/LICENSE).