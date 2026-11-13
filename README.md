# Biblioteca Exkyn.Core

[![NuGet Version](https://img.shields.io/nuget/v/Exkyn.Core.svg?style=flat-square&logo=nuget)](https://www.nuget.org/packages/Exkyn.Core/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Exkyn.Core.svg?style=flat-square&logo=nuget)](https://www.nuget.org/packages/Exkyn.Core/)
[![Build & Publish](https://img.shields.io/github/actions/workflow/status/dmodesigner/exkyn.core/dotnet.yml?branch=master&style=flat-square&logo=github)](https://github.com/dmodesigner/exkyn.core/actions)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg?style=flat-square)](https://github.com/dmodesigner/Exkyn.Core/blob/master/LICENSE)

Uma biblioteca construída em **.NET** para uso geral dentro de projetos C#, oferecendo extensões de tipos nativos, manipuladores de arquivos, criptografia e cliente HTTP tipado para consumo de APIs JSON.

---

## 📦 Instalação

Você pode instalar este pacote pelo **NuGet** utilizando a CLI do .NET:

```bash
dotnet add package Exkyn.Core
```

Ou através do **Package Manager Console** no Visual Studio:

```powershell
Install-Package Exkyn.Core
```

---

## 🧭 Estrutura do Projeto

O projeto é dividido em três áreas principais:

- **Extensions** (`Exkyn.Core.Extensions`) — Métodos de extensão para conversão, datas, horários, formatação monetária, limpeza de strings e validação de documentos.
- **Helpers** (`Exkyn.Core.Helpers`) — Utilitários estáticos para manipulação de diretórios, arquivos, criptografia (SHA-256 / AES-256) e gravação de logs.
- **Clients** (`Exkyn.Core.Clients`) — Cliente HTTP genérico para consumo de APIs REST em formato JSON.

---

## 🛠️ Extensions (`using Exkyn.Core.Extensions;`)

### `ConvertExtension`
Amplia as possibilidades de conversão, permitindo transformar objetos em string XML (e vice-versa) ou converter listas tipadas (`List<T>` e `IList<T>`) em `DataTable`.

```csharp
using System.Data;
using Exkyn.Core.Extensions;

// Serializar objeto para string XML e desserializar de volta
string xml = meuObjeto.ObjectToString();
MeuObjeto? objetoRecuperado = xml.StringToObject<MeuObjeto>();

// Converter lista para DataTable
DataTable tabela = minhaLista.ListToDataTable();
```

### `ExpirationDayExtension`
Adiciona dias a uma data de forma corrida ou considerando apenas dias úteis (com suporte opcional a lista de feriados).

```csharp
using Exkyn.Core.Extensions;

DateTime dataBase = DateTime.Now;
var feriados = new List<DateTime>
{
    new(2026, 11, 15),
    new(2026, 11, 20)
};

// Acrescenta 10 dias corridos
DateTime vencimentoCorrido = dataBase.CalendarDays(10);

// Acrescenta 5 dias úteis (pula sábados e domingos)
DateTime vencimentoUtil = dataBase.WorkingDays(5);

// Acrescenta 5 dias úteis pulando finais de semana e feriados informados
DateTime vencimentoSemFeriados = dataBase.WorkingDays(5, feriados);
```

### `HourExtension`
Permite ajustar uma data para o horário inicial (`00:00:00`) ou final (`23:59:59`) do dia, preservando o `DateTimeKind`.

```csharp
using Exkyn.Core.Extensions;

DateTime hoje = DateTime.Now;

DateTime inicioDoDia = hoje.StartTime(); // Retorna yyyy-MM-dd 00:00:00
DateTime fimDoDia = hoje.EndTime();      // Retorna yyyy-MM-dd 23:59:59
```

### `MoneyExtension`
Formata valores numéricos (`decimal` e `double`) no padrão monetário brasileiro (`pt-BR`) ou na cultura desejada.

```csharp
using Exkyn.Core.Extensions;

decimal valorDecimal = 1234.56m;
double valorDouble = 987.65;

string real = valorDecimal.Money();          // "R$ 1.234,56"
string dolar = valorDecimal.Money("en-US");  // "$1,234.56"
string euro = valorDouble.Money("es-ES");    // "987,65 €"
```

### `NoFormattingExtension`
Remove caracteres especiais, espaços e pontuações de uma string, mantendo apenas letras e números.

```csharp
using Exkyn.Core.Extensions;

string? cpfLimpo = "123.456.789-01".NoFormatting();     // "12345678901"
string? cnpjLimpo = "12.345.678/0001-99".NoFormatting(); // "12345678000199"
```

### `UpperFirstLetterExtension`
Remove espaços excedentes nas extremidades e deixa a primeira letra do texto em maiúscula.

```csharp
using Exkyn.Core.Extensions;

string? texto = "exkyn core".UpperFirstLetter(); // "Exkyn core"
```

### `ValidateExtension`
Permite validar **CPF**, **CNPJ**, **PIS/PASEP** (com ou sem máscara) e endereços de **E-mail**.

```csharp
using Exkyn.Core.Extensions;

bool cpfValido = "529.982.247-25".ValidateCPF();         // true
bool cnpjValido = "11.222.333/0001-81".ValidateCNPJ();   // true
bool pisValido = "120.56874.10-7".ValidatePIS();         // true
bool emailValido = "contato@exkyn.com".ValidateEmail();  // true
```

---

## ⚙️ Helpers (`using Exkyn.Core.Helpers;`)

### `DirectoryHelper`
Permite garantir a barra final em caminhos, criar diretórios caso não existam e listar todos os arquivos existentes no diretório e subdiretórios.

```csharp
using Exkyn.Core.Helpers;

string caminho = @"C:\Temp\MeuProjeto";

// Garante a barra invertida ao final: "C:\Temp\MeuProjeto\"
string caminhoComBarra = DirectoryHelper.AddSlashEnd(caminho);

// Cria o diretório se não existir
DirectoryHelper.Create(caminho);

// Lista os nomes de todos os arquivos contidos no diretório e subpastas
List<string> arquivos = DirectoryHelper.ListDirectoryFile(caminho);
```

### `EncryptHelper`
Permite gerar hashes irreversíveis usando **SHA-256** ou criptografar/descriptografar textos usando **AES-256** (CBC / PKCS7) via parâmetros diretos ou variáveis de ambiente (`Base64Key` e `Base64Vector`).

```csharp
using Exkyn.Core.Helpers;

// 1. Hash SHA-256 (retorna string hexadecimal de 64 caracteres)
string hashSha256 = EncryptHelper.SHA("MinhaSenhaSecreta");

// 2. AES-256 informando Chave (32 bytes em Base64) e Vetor IV (16 bytes em Base64)
string textoCifrado = EncryptHelper.AesEncryptor("Dados sensíveis", base64Key, base64Vector);
string textoOriginal = EncryptHelper.AesDecryptor(textoCifrado, base64Key, base64Vector);

// 3. AES-256 utilizando variáveis de ambiente ("Base64Key" e "Base64Vector")
string cifradoEnv = EncryptHelper.AesEncryptor("Dados sensíveis");
string originalEnv = EncryptHelper.AesDecryptor(cifradoEnv);
```

### `FileHelper`
Verifica a existência de arquivos, cria arquivos vazios, escreve linhas (em UTF-8 ou `Encoding` customizado) e lê todo o conteúdo textual.

```csharp
using System.Text;
using Exkyn.Core.Helpers;

string diretorio = @"C:\Temp\Arquivos";
string arquivo = "relatorio.txt";

// Cria o arquivo vazio caso não exista
FileHelper.Create(diretorio, arquivo);

// Verifica se o arquivo existe
bool existe = FileHelper.Exist(diretorio, arquivo);

// Escreve uma nova linha no arquivo (UTF-8 por padrão ou Encoding específico)
FileHelper.Write(diretorio, arquivo, "Primeira linha do relatório");
FileHelper.Write(diretorio, arquivo, "Segunda linha em Latin1", Encoding.Latin1);

// Lê todo o conteúdo do arquivo
string conteudo = FileHelper.ReadFile(diretorio, arquivo);
```

### `LogHelper`
Permite gravar logs de mensagens simples ou detalhes completos de exceções (`Message`, `Source`, `HelpLink`, `InnerException` até 2 níveis e `StackTrace`) em arquivos de texto.

```csharp
using Exkyn.Core.Helpers;

string pastaLog = @"C:\Logs\Aplicacao";

// Grava uma mensagem simples
LogHelper.Save(pastaLog, "eventos.log", "Processamento iniciado com sucesso.");

// Grava todos os detalhes de uma Exception
try
{
    throw new InvalidOperationException("Falha ao processar registro.");
}
catch (Exception ex)
{
    LogHelper.Save(pastaLog, "erros.log", ex);
}
```

---

## 🌐 Clients (`using Exkyn.Core.Clients;`)

### `ApiClient`
Permite consumir APIs externas configuradas para o formato JSON utilizando `HttpClient` injetado e suporte a `CancellationToken`. Possui métodos assíncronos `GET` e `POST` que retornam um objeto único ou uma lista devidamente desserializada.

```csharp
using Exkyn.Core.Clients;

using var httpClient = new HttpClient();

// 1. Consultas GET (Objeto único ou Lista)
var clientConsulta = new ApiClient<ProdutoResponse>(httpClient);

ProdutoResponse? produto = await clientConsulta.GetAsync("https://api.seudominio.com/produtos/1");
List<ProdutoResponse>? listaProdutos = await clientConsulta.GetListAsync("https://api.seudominio.com/produtos");

// 2. Requisições POST (Envia TEntrada e recebe TSaida ou List<TSaida>)
var clientEnvio = new ApiClient<CriarProdutoRequest, ProdutoResponse>(httpClient);
var novoProduto = new CriarProdutoRequest("Teclado Mecânico", 350.00m);

ProdutoResponse? produtoCriado = await clientEnvio.PostAsync("https://api.seudominio.com/produtos", novoProduto);
List<ProdutoResponse>? loteCriado = await clientEnvio.PostListAsync("https://api.seudominio.com/produtos/lote", novoProduto);
```

---

## 👤 Autor

- [@Daniel Moura](https://github.com/dmodesigner/)

---

## 📄 Licença

Este projeto é oferecido sob a licença **MIT**, sendo livre o seu uso pessoal ou comercial.

É disponibilizado sem garantias de qualquer tipo, sendo de sua total responsabilidade o seu uso. Para maiores detalhes, consulte o arquivo de [licença](https://github.com/dmodesigner/Exkyn.Core/blob/master/LICENSE).