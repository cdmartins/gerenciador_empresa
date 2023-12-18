using System;
using System.Globalization;
using System.IO;
using System.Text;

class GerenciarEmpresa
{
    Empresa empresa_matriz;

    private bool continueRunning = true;

    public bool CarregarDadosFuncionarios(string funcionarios_dados)
    {
        string[] linhas = File.ReadAllLines(funcionarios_dados);

        foreach (string linha in linhas)
        {
            string[] dados = linha.Split(";");

            string nome = dados[0];
            string cargo = dados[1];
            int salario = int.Parse(dados[2]);
            DateTime data_de_entrada = DateTime.ParseExact(dados[3],"M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
            int codigo_filial = int.Parse(dados[4]);

            Funcionario novo_funcionario = new(nome, cargo, salario, data_de_entrada, codigo_filial);

            empresa_matriz.CadastrarFuncionario(novo_funcionario);

        }
        return true;

    }

    public bool CarregarDadosFiliais(string filiais_dados)
    {
        string[] linhas = File.ReadAllLines(filiais_dados);

        foreach (string linha in linhas)
        {
            string[] dados = linha.Split(";");

            string nome = dados[0];
            DateTime data_criacao = DateTime.ParseExact(dados[1], "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
            int identificacao = int.Parse(dados[2]);
            string cnpj = dados[3];
            int quantidade_funcionarios = int.Parse(dados[4]);
            string filial_estado = dados[5];

            Filial nova_filial = new(nome, data_criacao, identificacao, cnpj, quantidade_funcionarios, filial_estado);

            empresa_matriz.CadastrarFilial(nova_filial);
        }
        return true;
    }


    public bool GravarDadosFuncionarios(string funcionarios_dados)
    {
        using (StreamWriter writer = new StreamWriter(funcionarios_dados))
        {
            foreach (Funcionario f in empresa_matriz.getListaFuncionarios())
            {
                string linha = string.Format("{0};{1};{2};{3};{4}", f.nome, f.cargo, f.salario, f.data_entrada, f.codigo_filial);
                writer.WriteLine(linha);
            }

        }

        return true;
    }

    public bool GravarDadosFiliais(string filiais_dados)
    {
        using (StreamWriter writer = new StreamWriter(filiais_dados))
        {
            foreach (Filial f in empresa_matriz.getListaFiliais())
            {
                string linha = string.Format("{0};{1};{2};{3};{4};{5}", f.nome, f.data_criacao, f.identificacao, f.cnpj, f.quantidade_funcionarios, f.filial_estado);
                writer.WriteLine(linha);
            }

        }

        return true;
    }


    public GerenciarEmpresa()
    {
        empresa_matriz = new Empresa("ENTERPRISE OTAKU", new DateTime(2000, 01, 01), 0, "65.777753/0001-47");
    }

    public void Menu()
    {

        string option = "";
        do
        {
            limpaConsoleExibeLogo();
            System.Console.WriteLine("Selecione uma opção abaixo:");
            System.Console.WriteLine("[0] - Sair");
            System.Console.WriteLine("[1] - Cadastrar filial");
            System.Console.WriteLine("[2] - Cadastrar colaborador");
            System.Console.WriteLine("[3] - Plano de carreira");
            System.Console.WriteLine("[4] - Relatórios");
            System.Console.WriteLine("[5] - Gravar Funcionarios/Filiais Cadastradas Até o Momento");
            System.Console.Write("Digite sua opção: ");
            option = Console.ReadLine()!;

            switch (option)
            {
                case "0":
                    System.Console.WriteLine("Programa encerrado");
                    continueRunning = false;
                    break;

                case "1":
                    limpaConsoleExibeLogo();
                    Console.Write("Digite o nome da filial: ");
                    string nome = Console.ReadLine()!;
                    Console.Write("Digite a data de criação da filial (ano/mes/dia): ");
                    DateTime data_criacao = DateTime.Parse(Console.ReadLine()!);
                    Console.Write("Digite o código da filial: ");
                    int identificacao = int.Parse(Console.ReadLine()!);
                    Console.Write("Digite o CNPJ 65.777753/0001:--: ");
                    string cnpj = Console.ReadLine()!;
                    Console.Write("Digite a quantidade de funcionários: ");
                    int quantidade_funcionarios = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Digite o estado da filial [XY]: ");
                    string filial_estado = Console.ReadLine()!;

                    // Criar uma instancia de Filial e adicionar a lista de filiais dentro da empresa
                    Filial nova_filial = new(nome, data_criacao, identificacao, cnpj, quantidade_funcionarios, filial_estado);
                    empresa_matriz.CadastrarFilial(nova_filial);

                    Console.Write("Filial cadastrada com sucesso!!\n\nPressione qualquer tecla para voltar ao Menu inicial;");
                    Console.ReadKey();

                    break;

                case "2":
                    limpaConsoleExibeLogo();
                    System.Console.Write("Nome: ");
                    string nome_funcionario = Console.ReadLine()!;
                    System.Console.Write("Cargo: ");
                    string cargo = Console.ReadLine()!;
                    System.Console.Write("Salário: ");
                    float salario = float.Parse(Console.ReadLine()!);

                    Console.WriteLine("\nDATA DE ENTRADA");
                    Console.Write("Dia: ");
                    int dia = int.Parse(Console.ReadLine()!);
                    Console.Write("Mês: ");
                    int mes = int.Parse(Console.ReadLine()!);
                    Console.Write("Ano: ");
                    int ano = int.Parse(Console.ReadLine()!);

                    limpaConsoleExibeLogo();
                    empresa_matriz.ExibirCodigoFiliais();
                    Console.WriteLine("Digite o código da empresa que deseja cadastrar este funcionário:");
                    int codigo_filial = int.Parse(Console.ReadLine()!);

                    Funcionario novo_funcionario = new(nome_funcionario, cargo, salario, new DateTime(ano, mes, dia), codigo_filial);

                    empresa_matriz.CadastrarFuncionario(novo_funcionario);
                    limpaConsoleExibeLogo();
                    Console.WriteLine($"\nFuncionário {novo_funcionario.nome} cadastrado com sucesso!\n\n");

                    novo_funcionario.ExibirTempoNaEmpresaEmAnos();
                    novo_funcionario.ExibirDadosAposCadastro();


                    Console.WriteLine("\nPressione qualquer tecla para retornar ao menu inicial");
                    Console.ReadKey();

                    break;

                case "3":
                    limpaConsoleExibeLogo();
                    empresa_matriz.ExibirFuncionarios();
                    Console.WriteLine("Digite o número referente ao funcionário para visualizar seu plano de carreira.");
                    int resposta = int.Parse(Console.ReadLine()!);
                    limpaConsoleExibeLogo();

                    Funcionario funcionario_plano_carreira = empresa_matriz.GetFuncionario(resposta - 1);

                    PlanoCarreira novo_plano = new(funcionario_plano_carreira);
                    novo_plano.ExibirFuncionarios(empresa_matriz.getListaFuncionarios());
                    limpaConsoleExibeLogo();
                    novo_plano.InfoDoFuncionario(funcionario_plano_carreira, novo_plano);
                    novo_plano.SubirCargo();

                    Console.ReadKey();


                    break;

                case "4":
                    do
                    {
                        limpaConsoleExibeLogo();
                        System.Console.WriteLine("[1] - Relatório de funcionários");
                        System.Console.WriteLine("[2] - Relatório de filiais");
                        System.Console.WriteLine("[3] - Voltar ao menu principal");
                        System.Console.Write("Digite sua opção: \n\n");
                        int choose = int.Parse(Console.ReadLine()!);

                        switch (choose)
                        {
                            case 1:
                                limpaConsoleExibeLogo();
                                empresa_matriz.ExibirFuncionariosPorFilial(empresa_matriz);

                                Console.WriteLine("\n\nPressione qualquer tecla para retornar ao menu inicial");
                                Console.ReadKey();
                                break;

                            case 2:
                                limpaConsoleExibeLogo();
                                empresa_matriz.ExibirDadosFiliais();

                                Console.WriteLine("\n\nPressione qualquer tecla para retornar ao menu inicial");
                                Console.ReadKey();
                                break;

                            default:
                                System.Console.WriteLine("Opção inválida");
                                break;
                        }
                    } while (option != "4");
                    break;

                case "5":
                    GravarDadosFuncionarios("banco_de_dados/FuncionariosCadastrados.txt");
                    GravarDadosFiliais("banco_de_dados/FiliaisCadastradas.txt");
                    Console.WriteLine("\nDados gravados com sucesso! Pressione qualquer teclar continuar.");
                    Console.ReadKey();

                    break;

                default:
                    System.Console.WriteLine("Opção inválida");
                    break;
            }

        } while (continueRunning);
    }

    private void limpaConsoleExibeLogo()
    {
        Console.Clear();
        Logo();
    }

    private void Logo()
    {
        Console.WriteLine(@"
█████████████████████████████████████████████████████████████████████████████████████████████
█▄─▄▄─█▄─▀█▄─▄█─▄─▄─█▄─▄▄─█▄─▄▄▀█▄─▄▄─█▄─▄▄▀█▄─▄█─▄▄▄▄█▄─▄▄─███─▄▄─█─▄─▄─██▀▄─██▄─█─▄█▄─██─▄█
██─▄█▀██─█▄▀─████─████─▄█▀██─▄─▄██─▄▄▄██─▄─▄██─██▄▄▄▄─██─▄█▀███─██─███─████─▀─███─▄▀███─██─██
▀▄▄▄▄▄▀▄▄▄▀▀▄▄▀▀▄▄▄▀▀▄▄▄▄▄▀▄▄▀▄▄▀▄▄▄▀▀▀▄▄▀▄▄▀▄▄▄▀▄▄▄▄▄▀▄▄▄▄▄▀▀▀▄▄▄▄▀▀▄▄▄▀▀▄▄▀▄▄▀▄▄▀▄▄▀▀▄▄▄▄▀▀");
    }

    public static void Main(string[] args)
    {
        GerenciarEmpresa meu_gerenciador = new GerenciarEmpresa();

        ///Metodo pra carregar dados existentes no banco de dados:
        meu_gerenciador.CarregarDadosFiliais(@"banco_de_dados\FiliaisCadastradas.txt");
        meu_gerenciador.CarregarDadosFuncionarios(@"banco_de_dados\FuncionariosCadastrados.txt");

        meu_gerenciador.Menu();
    }
}
