using System.Xml.Serialization;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace OtimizadorVideo
{
    public partial class Form1 : Form
    {

        List<string> videolist = new List<string>();
        public Form1()
        {
            InitializeComponent();
            Form1_Load();
        }

        private void deletePathVideo()
        {
            if (Directory.Exists(Path.Combine(Application.StartupPath, "videocarregado")) == true)
            {
                Log("Pasta de videos carregados encontrada. Hora de deletar para começar do zero");
                Directory.Delete(Path.Combine(Application.StartupPath, "videocarregado"), true);
            }
        }
        private void deletePathFrame()
        {
            if (Directory.Exists(Path.Combine(Application.StartupPath, "frames")) == true)
            {
                Log("Pasta de frames carregados encontrada. Hora de deletar para começar do zero");
                Directory.Delete(Path.Combine(Application.StartupPath, "frames"), true);
            }
        }


        private async Task ExtrairFrames(string caminhodovideo)
        {
            await Task.Run(() =>
            {
                progressoLabel("extração de frames do video");
                Log($"Iniciando a extração de frames do video: {Path.GetFileName(caminhodovideo)}");
                progressoBar(10);
                try
                {
                    Log("Definindo a saida do video...");
                    string saida = Path.Combine(Application.StartupPath, "frames");
                    progressoBar(20);
                    if (Directory.Exists(saida) == false)
                    {
                        Log("A pasta para guardar os frames não existe. Criando uma nova");
                        Directory.CreateDirectory(saida);
                        Log("Pasta criada");
                    }
                    Log($"Pasta de saida definida: {saida}");
                    progressoBar(30);
                    VideoCapture video = new VideoCapture(caminhodovideo);
                    double totalFrames = video.Get(CapProp.FrameCount);
                    Mat frame = new Mat();
                    Log("Preparando os objetos para a extração dos frames do video");
                    progressoBar(40);
                    int contador = 0;

                    Log("Iniciando a extração dos frames do video...");
                    progressoBar(50);
                    while (true)
                    {
                        video.Read(frame);
                        if (frame.IsEmpty)
                        {
                            Log("Todos os frames do video foram extraídos.");
                            progressoBar(80);
                            break;
                        }
                        Log($"Extraindo frame: {contador}");
                        frame.Save(Path.Combine(saida, $"frame_{contador:D4}.jpg"));
                        contador++;

                        double progressoRelativo = 50 + (contador / totalFrames) * 50;
                        progressoBar((int)progressoRelativo);
                    }
                    Log($"Extração completa. {contador} frames extraídos e salvos na pasta: {saida}");
                    video.Dispose();
                    frame.Dispose();
                    progressoLabel("");
                }
                catch (Exception ex)
                {
                    progressoLabel("");
                    Log($"Ocorreu um erro no enquanto extraia os frames: {ex.Message}");
                }
            });
        }

        private void Form1_Load()
        {
            Log("Iniciando o Otimizador de Video");
            progressoLabel("Carregando o Otimizador");
            progressoBar(10);
            deletePathVideo();
            deletePathFrame();
            progressoBar(50);
            Log("Carregamento do Otimizador Completo");
            progressoLabel("");
            Log("Pronto para uso");
        }

        private void progressoLabel(string text)
        {
            if (processolabel.InvokeRequired)
            {
                processolabel.Invoke((MethodInvoker)(() => progressoLabel(text)));
            }
            else
            {
                processolabel.Text = $"Processo: {text}";
                progressoBar(0); // Se quiser zerar a barra junto
            }
        }
        private void progressoBar(int valor)
        {
            if (processobar.InvokeRequired)
            {
                processobar.Invoke((MethodInvoker)(() => progressoBar(valor)));
            }
            else
            {
                if (valor >= 0 && valor <= 100)
                {
                    processobar.Value = valor;
                }
                else
                {
                    Log($"Valor de progresso inválido: {valor}. Deve estar entre 0 e 100.");
                }
            }
        }

        private void Log(string mensagem)
        {
            if (consoletext.InvokeRequired)
            {
                consoletext.Invoke((MethodInvoker)(() => Log(mensagem)));
            }
            else
            {
                consoletext.AppendText($"[ {DateTime.Now:HH:mm:ss} - {mensagem} ]" + Environment.NewLine);
            }
        }

        private void combolista()
        {
            combovideo.Items.Clear();
            foreach (var item in videolist)
            {
                combovideo.Items.Add(Path.GetFileName(item));
            }

        }

        private void carregarvideo_Click(object sender, EventArgs e)
        {
            carregarArquivoMp4();
        }

        private bool carregarArquivoMp4()
        {
            progressoLabel("Carregando o Video");
            Log("Começando o Carregamento do Video");
            try
            {
                Log("Preparando para receber o arquivo");
                progressoBar(10);
                OpenFileDialog inputjanela = new OpenFileDialog
                {
                    Title = "Selecione o arquivo de vídeo",
                    Filter = "Arquivos de Vídeo|*.mp4"
                };
                progressoBar(20);
                Log($"Selecionando o tipo de Arquivo: {inputjanela.Filter}");
                if (inputjanela.ShowDialog() == DialogResult.OK)
                {
                    progressoBar(30);
                    Log("Arquivo recebido com sucesso");
                    string input_path = inputjanela.FileName;
                    string input_name = Path.GetFileName(input_path);
                    string output_pathpaste = Path.Combine(Application.StartupPath, "videocarregado");
                    progressoBar(40);
                    if (Directory.Exists(output_pathpaste) == false)
                    {
                        Log("A pasta para guardar o video não existe. Hora de criar uma nova!");
                        Directory.CreateDirectory(output_pathpaste);
                        Log("Pasta criada");
                    }
                    string output_path = Path.Combine(output_pathpaste, input_name);
                    progressoBar(50);
                    if (File.Exists(output_path))
                    {
                        Log($"O arquivo {input_name} já existe na pasta. O novo arquivo será renomeado.");
                        output_path = Path.Combine(output_pathpaste, $"{DateTime.Now:HH_mm_ss_}" + input_name);
                    }
                    progressoBar(60);
                    Log("Preparando para guardar o Arquivo para uso do programa");
                    File.Copy(input_path, output_path, true);
                    progressoBar(90);
                    Log($"Arquivo armazenado com sucesso: {Path.GetFileName(output_path)}");
                    videolist.Add(output_path);
                    progressoBar(100);
                    Log("Carregamento completo. Adicionando o arquivo a lista de videos disponiveis");
                    combolista();
                    inputjanela.Dispose();
                    progressoLabel("");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Log($"Não Foi possivel carregar o Arquivo. O seguinte erro apareceu: {ex.Message}");
                progressoLabel("");
                return false;
            }

        }

        private async void btextrair_Click(object sender, EventArgs e)
        {
            progressoLabel("Preparando objetos para a extracao.");
            progressoBar(10);
            Log("Verificando a Integridade das opcoes.");
            progressoBar(20);
            if (combovideo.SelectedItem != null && string.IsNullOrEmpty(combovideo.SelectedItem.ToString()) == false && combovideo.SelectedItem.ToString() != "")
            {
                Log("Preparando os dados necessarios para a operacao");
                string videotrabalhado = null;
                progressoBar(30);
                foreach (var item in videolist)
                {
                    if (Path.GetFileName(item) == combovideo.SelectedItem.ToString())
                    {
                        videotrabalhado = item;
                        Log($"Selecionando o video: {Path.GetFileName(videotrabalhado)}");
                        progressoBar(60);
                        break;
                    }
                }
                progressoBar(70);
                Log("Verificando a integridade do video.");
                if (string.IsNullOrEmpty(videotrabalhado) == false)
                {
                    progressoBar(100);
                    Log("Solicitando o inicio do processo de Extração.");
                    await ExtrairFrames(videotrabalhado);
                }
            }
            else
            {
                Log("Nenhum video selecionado. Por favor, selecione um video na lista.");
                progressoLabel("");
            }
        }
    }
}
