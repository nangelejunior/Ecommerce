using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Ecommerce
{
    public class Utilitario
    {
        // variável de conexão com o banco de dados
        private SqlConnection con;
        // instância da classe banco
        private Banco bd = new Banco();
        // variável de comandos SQL
        private SqlCommand cmd;

        // método que executa comandos SQL
        public bool executeSQL(string sql)
        {
            // executou recebe true ou false
            // true se excutou com sucesso
            // false se ocorreu algum erro
            bool executou = false;

            // número de linhas afetadas pelo comando ExecuteNonQuery
            int numLinhasAf = 0;

            // tenta executar o comando SQL
            try
            {
                // instância da classe SqlConnection
                con = new SqlConnection();
                // o objeto con recebe a conexão existente
                con = bd.getConexao();
                // instância a classe SqlCommand
                cmd = new SqlCommand();
                // passa a conexão para o objeto
                cmd.Connection = con;
                // passa o comando SQL ao objeto cmd
                cmd.CommandText = sql;
                // executa o comando SQL
                numLinhasAf = cmd.ExecuteNonQuery();
                
                // se o número de linhas afetatas for maior que 0
                if (numLinhasAf > 0)
                {
                    executou = true; // executou recebe true
                }
                else
                {
                    executou = false; // executou recebe false
                } // fim do if..else
            }
            catch (SqlException ex)
            {
                // se ocorreu um erro ao inserir
                executou = false; // executou recebe false
            }
            finally
            {
                // elimina o objeto da memória
                cmd.Dispose();
            } // fim do try..cath..finally

            return executou;
        } // fim do método executeSQL

        // método que faz a validação do cpf
        public bool validarCPF(string cpf)
        {
            // vetor que armazena os número do cpf
            int[] numeros = new int[11];

            // variável que armazena a primeira soma
            int soma1 = 0;
            // variável que armazena a segunda soma
            int soma2 = 0;

            // variável que armazena o primeiro digito verificador
            int d1 = 0;
            // variável que armazena o segundo digito verificador
            int d2 = 0;

            // loop que insere os números no vetor
            for (int i = 0; i < 11; i++)
            {
                // se ouver algum erro ao tentar converter o número
                if (!int.TryParse(cpf[i].ToString(), out numeros[i]))
                {
                    // encerra o processamento e retorna false
                    return false;
                } // fim do if
            } // fim do for

            // loop que determina a primeira soma
            for (int j = 0, m = 10; j < 9; j++, m--)
            {
                // soma1 = 10a + 9b + 8c + 7d + 6e + 5f + 4g + 3h + 2i
                soma1 += numeros[j] * m;
            } // fim do for

            // se o resto da divisão da soma1 por 11 for igual a 0 ou 1
            if ((soma1 % 11 == 0) || (soma1 % 11 == 1))
            {
                d1 = 0; // d1 recebe 0
            }
            else
            {
                // se não, recebe 11 menos o resto
                d1 = 11 - (soma1 % 11);
            } // fim do if..else

            // for que determina a segunda soma
            for (int k = 0, m = 11; k < 10; k++, m--)
            {
                // soma2 = 11a + 10b + 9c + 8d + 7e + 6f + 5g + 4h + 3i + 2j
                soma2 += numeros[k] * m;
            } // fim do for

            // se o resto da divisão da soma1 por 11 for igual a 0 ou 1
            if ((soma2 % 11 == 0) || (soma2 % 11 == 1))
            {
                d2 = 0;
            }
            else
            {
                // se não, recebe 11 menos o resto
                d2 = 11 - (soma2 % 11);
            } // fim do if..else

            // d1 for igual ao primeiro digito verificador e
            // d2 for igual ao segundo digito verificador
            if ((numeros[9] == d1) && (numeros[10] == d2))
            {
                return true; // retorna true
            }
            else
            {
                // se não
                return false; // retorna false
            } // fim do if..else
        } // fim do método validarCPF

        // método que válida e-mail
        public bool validarEmail(string email)
        {
            // Expressão regular que vai validar os e-mails
            string emailRegex = @"^(([^<>()[\]\\.,;áàãâäéèêëíìîïóòõôöúùûüç:\s@\""]+"
            + @"(\.[^<>()[\]\\.,;áàãâäéèêëíìîïóòõôöúùûüç:\s@\""]+)*)|(\"".+\""))@"
            + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|"
            + @"(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$";

            // Instância da classe Regex, passando como 
            // argumento sua Expressão Regular 
            Regex rx = new Regex(emailRegex);

            // Método IsMatch da classe Regex que retorna
            // verdadeiro caso o e-mail passado estiver
            // dentro das regras da sua regex.
            return rx.IsMatch(email);
        } // fim do método validarEmail
    } // fim da classe Utilitario
}
