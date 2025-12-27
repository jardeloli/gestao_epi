A API de Gerenciamento de EPI (Equipamentos de Proteção Individual) é uma aplicação backend desenvolvida em C# com ASP.NET, cujo objetivo é controlar, organizar e rastrear a distribuição de EPIs dentro de uma organização, garantindo conformidade com normas de segurança do trabalho e melhor gestão dos recursos.

A API permite o cadastro de usuários, registro de EPIs, controle de retiradas e devoluções, além da gestão de permissões e perfis de acesso, assegurando que apenas usuários autorizados possam executar determinadas operações no sistema.

🎯 Objetivos do Sistema

Garantir o controle eficiente da entrega de EPIs aos colaboradores.
Reduzir perdas e extravios de equipamentos.
Manter histórico de retiradas por usuário.
Facilitar auditorias e fiscalizações de segurança do trabalho.
Centralizar informações em uma API segura e escalável.

⚙️ Principais Funcionalidades:

Cadastro de Usuários.
Criação, edição e exclusão de usuários.
Autenticação e controle de acesso.
Gerenciamento de EPIs.
Cadastro de equipamentos.
Controle de estoque.
Atualização de informações dos EPIs.
Controle de Retirada.
Registro de retirada de EPI por usuário.
Associação com data, quantidade e responsável.
Histórico de movimentações.
Autenticação e Autorização.
Autenticação baseada em credenciais.
Perfis e permissões de acesso.
Proteção de rotas sensíveis da API.

🛠️ Tecnologias Utilizadas

Linguagem: C#.
Framework: ASP.NET Core.
ORM: Entity Framework Core.
Banco de Dados: MySQL.
Padrão Arquitetural: REST.
Formato de Comunicação: JSON sobre HTTP/HTTPS.

🏗️ Estrutura da Aplicação

A API segue uma arquitetura organizada em camadas, promovendo manutenção e escalabilidade:
Controllers: Responsáveis por receber e responder requisições HTTP.
Services (ou Regras de Negócio): Contêm a lógica do sistema.
Models / Entities: Representam as tabelas do banco de dados.
DTOs: Transferência segura de dados entre camadas.
Data (DbContext): Comunicação com o banco via Entity Framework.

🔐 Segurança

Senhas armazenadas de forma criptografada.
Validação de dados de entrada.
Controle de acesso baseado em perfis.
Rotas protegidas por autenticação.
