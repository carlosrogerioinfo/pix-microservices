using System.ComponentModel;

namespace Pix.Microservices.Domain.Enums
{
    public enum UserType
    {
        [Description("Administrador")]
        Administrator = 1,
        [Description("Usuário")]
        User = 2,
    }

    public enum AccountType
    {
        [Description("Conta Corrente")]
        CheckingAccount = 1,
        [Description("Conta Poupanca")]
        DepositAccount = 2,
    }

    public enum ErrorCodeType
    {
        [Description("Nenhum registro encontrado")]
        NotFount = 1,
        [Description("Banco ja cadastrado")]
        BankExists = 2,
        [Description("Banco ja cadastrado")]
        GenericError = 3
    }

    public enum StatusCodeType
    {
        [Description("Recebida")]
        Approved = 1,
        [Description("Pendente")]
        Pending = 2,
        [Description("Cancelada")]
        Canceled = 3,
        [Description("Erro")]
        Error = 4
    }


}
