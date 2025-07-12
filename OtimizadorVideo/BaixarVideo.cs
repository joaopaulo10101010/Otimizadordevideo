using AngleSharp.Dom.Events;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using ImageMagick;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using System.Xml.Serialization;
using Youtubelib;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using Application = System.Windows.Forms.Application;
using File = System.IO.File;

namespace OtimizadorVideo
{
    public partial class BaixarVideo : Form
    {
        bool onprocess = false;
        YoutubeVideo youtubevideo = new YoutubeVideo("");

        List<string> videolist = new List<string>();
        public BaixarVideo()
        {
            InitializeComponent();
            Form1_Load();
            youtubevideo.AlterarFfmpeg(Path.Combine(Application.StartupPath, "ffmpeg/bin/ffmpeg.exe"));
        }

        private void deletePathVideo()
        {
            if (Directory.Exists(Path.Combine(Application.StartupPath, "videocarregado")) == true)
            {
                foreach (var proc in Process.GetProcessesByName("ffmpeg"))
                {
                    try
                    {
                        proc.Kill();
                        proc.WaitForExit();
                        Log("FFmpeg encerrado à força.");
                    }
                    catch (Exception ex)
                    {
                        Log("Erro ao matar FFmpeg: " + ex.Message);
                    }
                }
                Log("Pasta de videos carregados encontrada. Hora de deletar para começar do zero");
                Directory.Delete(Path.Combine(Application.StartupPath, "videocarregado"), true);

            }
            if (Directory.Exists(Path.Combine(Application.StartupPath, "videoootimizado")) == true)
            {
                Log("Pasta de videos otimizados encontrada. Hora de deletar para começar do zero");
                Directory.Delete(Path.Combine(Application.StartupPath, "videoootimizado"), true);
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

        private async Task JuntarFrames()
        {
            await Task.Run(() =>
            {
                Log("Iniciando a criação do video a partir dos frames extraídos.");
                progressoLabel("Criando o video.");
                string pastaFrames = Path.Combine(Application.StartupPath, "frames");
                string videopasta = Path.Combine(Application.StartupPath, "videoreduzido");
                string videoSaida = Path.Combine(Application.StartupPath, "videoreduzido", "video.mp4");
                progressoBar(10);
                if (Directory.Exists(videopasta) == false)
                {
                    Log("Pasta para guardar videos não foi encontrada, hora de criar uma.");
                    Directory.CreateDirectory(videopasta);
                }



                string[] arquivos = Directory.GetFiles(pastaFrames, "*.jpg").OrderBy(f => f).ToArray();
                progressoBar(20);
                Log($"Encontrados {arquivos.Length} frames na pasta: {pastaFrames}");
                if (arquivos.Length == 0)
                {
                    Log("Nenhum frame encontrado na pasta.");
                    return;
                }

                progressoBar(30);
                Mat primeiroFrame = CvInvoke.Imread(arquivos[0], ImreadModes.ColorRgb);
                Size tamanhoFrame = primeiroFrame.Size;


                VideoWriter escritor = new VideoWriter(
                videoSaida, VideoWriter.Fourcc('M', 'P', '4', 'V'), 30, tamanhoFrame, true);
                Log($"Criando o video: {videoSaida} com tamanho de frame: {tamanhoFrame.Width}x{tamanhoFrame.Height}");
                progressoBar(50);
                foreach (string caminho in arquivos)
                {
                    Mat img = CvInvoke.Imread(caminho, ImreadModes.ColorRgb);
                    escritor.Write(img);
                    img.Dispose();
                }
                Log("Todos os frames foram adicionados ao video.");
                progressoBar(90);

                Log("Vídeo gerado com sucesso.");
                progressoBar(100);
            });

        }

        private void otimizarVideo(string video_path)
        {
            try
            {
                Log($"Começando a otimização do frame: {Path.GetFileName(video_path)}");
                var configuracao = new MagickReadSettings();
                MagickImage magicimage = new MagickImage(video_path, configuracao);
                Log("Objetos para a otimização foram preparados.");
                magicimage.Strip();
                magicimage.Quality = 80;
                magicimage.Write(video_path);
                Log($"Otimização do frame foi concluida: {Path.GetFileName(video_path)}");
                magicimage.Dispose();
            }
            catch (Exception ex)
            {
                Log($"Ocorreu um erro durante a otimização do frame: {Path.GetFileName(video_path)}, o seguinte erro foi relatado: {ex.Message}");
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

                        Log("Otimizando o frame.");
                        otimizarVideo(Path.Combine(saida, $"frame_{contador:D4}.jpg"));


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

        private async Task ReduzirVideo(string videoEntrada, string qualidade = "28")
        {
            await Task.Run(() =>
            {
                if (string.IsNullOrEmpty(saidatools.Text))
                {
                    Log("Adicione um local para salvar o arquivo");
                    return;
                }
                try
                {
                    progressoLabel("Reduzindo o tamanho do video");
                    string videopasta = saidatools.Text;
                    string videoSaida = Path.Combine(videopasta, "video.mp4");
                    progressoBar(10);
                    if (Directory.Exists(videopasta) == false)
                    {
                        Log("Pasta para guardar videos não foi encontrada, hora de criar uma.");
                        Directory.CreateDirectory(videopasta);
                    }
                    progressoBar(20);

                    string ffmpegPath = Path.Combine(Application.StartupPath, "ffmpeg/bin/ffmpeg.exe");

                    if (!File.Exists(ffmpegPath))
                    {
                        Log("FFmpeg não foi encontrado.");
                        return;
                    }
                    progressoBar(30);
                    string argumentos = $"-i \"{videoEntrada}\" -vcodec libx264 -crf {qualidade} -preset slow -acodec aac -b:a 128k \"{videoSaida}\"";

                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = ffmpegPath,
                        Arguments = argumentos,
                        CreateNoWindow = true,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true
                    };
                    progressoBar(40);
                    try
                    {
                        using (Process processo = Process.Start(startInfo))
                        {
                            string erro = processo.StandardError.ReadToEnd();
                            string saida = processo.StandardOutput.ReadToEnd();

                            processo.WaitForExit();

                            if (!string.IsNullOrWhiteSpace(erro))
                                Log("FFmpeg erro: " + erro);

                            if (!string.IsNullOrWhiteSpace(saida))
                                Log("FFmpeg saída: " + saida);

                            if (processo.ExitCode == 0)
                                Log("Compressão de vídeo finalizada com sucesso.");
                            else
                                Log($"FFmpeg terminou com código de erro {processo.ExitCode}");
                        }
                        progressoBar(100);
                    }
                    catch (Exception ex)
                    {
                        Log($"Erro ao reduzir o vídeo: {ex.Message}");
                    }
                }
                catch (Exception ex)
                {
                    Log($"Ocorreu um erro ao reduzir o video: {ex.Message}");
                }
            });
        }


        private async void btextrair_Click(object sender, EventArgs e)
        {
            if (onprocess)
            {
                Log("Ja existe um processo em andamento, aguarde o termino dele para continuar.");
                return;
            }
            if (string.IsNullOrEmpty(saidatools.Text))
            {
                Log("Por favor, adicione um local para salvar os frames extraídos.");
                await LabelErro(saidapathoti);
                return;
            }
            try
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
                    progressoBar(100);
                    Log("Começando a criar o zip com os frames");
                    ZipFile.CreateFromDirectory(Path.Combine(Application.StartupPath, "frames"), Path.Combine(saidatools.Text,$"{DateTime.Now:HH_mm_ss_}frames.zip"));
                    Log("Zip criado, hora de limpar os arquivos utilizados");
                    deletePathFrame();

                    progressoLabel("");
                }
                else
                {
                    Log("Nenhum video selecionado. Por favor, selecione um video na lista.");
                    progressoLabel("");
                }
            }
            catch (Exception ex)
            {
                Log($"Ocorreu um erro durante o processo: {ex.Message}");
            }
            finally
            {
                progressoLabel("");
                onprocess = false;
            }
            
        }

        private async void otimizarbt_Click(object sender, EventArgs e)
        {
            if (onprocess)
            {
                Log("Um processo de otimização já está em andamento. Aguarde a conclusão antes de iniciar outro.");
                return;
            }
            if (string.IsNullOrEmpty(saidatools.Text))
            {
                Log("Por favor, adicione um local para salvar o video otimizado.");
                await LabelErro(saidapathoti);
                return;
            }
            try
            {
                onprocess = true;
                progressoLabel("Preparando objetos para a otimização.");
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
                        Log("Solicitando o inicio do processo de Otimização.");
                        await ReduzirVideo(videotrabalhado, qualidadebar.Value.ToString());
                    }
                    Log("Começando a criar o video");
                    progressoLabel("");
                    Log("Otimização completa.");
                }
                else
                {
                    Log("Nenhum video selecionado. Por favor, selecione um video na lista.");
                    progressoLabel("");
                }
            }catch (Exception ex)
            {
                Log($"Ocorreu um erro ao otimizar o video: {ex.Message}");
                progressoLabel("");
            }
            finally
            {
                onprocess = false;
                progressoLabel("");
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (onprocess)
            {
                Log("Um processo de Download do youtube ja esta acontecendo, espere ele terminar para baixar o proximo.");
                return;
            }
            if (string.IsNullOrEmpty(youtubelink.Text))
            {
                Log("Por favor, insira um link do YouTube.");
                await LabelErro(Labelyoutubelink);
                return;
            }
            if (string.IsNullOrEmpty(saidavideoyoutube.Text))
            {
                Log("Profavor, insira um local para salvar o video.");
                await LabelErro(labelsaidayoutube);
                return;
            }
            try
            {
                onprocess = true;
                progressoLabel("Baixando Video do youtube.");
                progressoBar(10);
                Log("Preparando para carregar o video.");
                string url = youtubelink.Text;
                progressoBar(20);
                Log("Carregando Url");
                if (await youtubevideo.TrocarUrl(url))
                {
                    Log($"URL do YouTube carregada com sucesso. Video: {await youtubevideo.GetTitulo()}");
                    Log("Preparando as ferramentas necessarias para o processo.");
                    progressoBar(50);
                    Log("Iniciando o download do video.");
                    string saidapath = Path.Combine(saidavideoyoutube.Text, $"{await youtubevideo.GetTitulo()}.mp4");
                    if (Path.Exists(saidapath))
                    {
                        Log("Ja existe um arquivo com o nome de saida, hora de renomear para um novo nome.");
                        saidapath = Path.Combine(saidavideoyoutube.Text, $"{DateTime.Now:HH_mm_ss_}{await youtubevideo.GetTitulo()}.mp4");
                    }
                    await youtubevideo.BaixarVideo(saidapath, true);
                    progressoBar(80);
                    Log("Download completo.");
                    progressoBar(100);
                    progressoLabel("");
                }
                else
                {
                    progressoLabel("");
                    Log("Falha ao carregar a URL do YouTube.");
                    onprocess = false;
                }
                onprocess = false;
            }
            catch (Exception ex)
            {
                Log($"Ocorreu o seguinte erro: {ex.ToString()}");
                Log("Ocorreu um erro ao Baixar o video, verifique o link e a saida do video");
                progressoLabel("");
                onprocess = false;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog
            {
                Description = "Selecione a pasta para salvar o video."
            };
            folderBrowserDialog.ShowDialog();
            if (!string.IsNullOrEmpty(folderBrowserDialog.SelectedPath))
            {
                saidavideoyoutube.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            youtubelink.Clear();
            youtubelink.Text = Clipboard.GetText();
        }

        private async Task LabelErro(Label label)
        {
            for (int i = 0; i < 3; i++)
            {
                label.ForeColor = Color.Red;
                await Task.Delay(500);
                label.ForeColor = Color.Black;
                await Task.Delay(500);
            }
        }

        private async void baixaraudio_Click(object sender, EventArgs e)
        {
            if (onprocess)
            {
                Log("Um processo de Download do youtube ja esta acontecendo, espere ele terminar para baixar o proximo.");
                return;
            }
            if (string.IsNullOrEmpty(youtubelink.Text))
            {
                Log("Por favor, insira um link do YouTube.");
                await LabelErro(Labelyoutubelink);
                return;
            }
            if (string.IsNullOrEmpty(saidavideoyoutube.Text))
            {
                Log("Profavor, insira um local para salvar o audio.");
                await LabelErro(labelsaidayoutube);
                return;
            }
            try
            {
                onprocess = true;
                progressoLabel("Baixando Audio do youtube.");
                progressoBar(10);
                Log("Preparando para carregar o Audio.");
                string url = youtubelink.Text;
                progressoBar(20);
                Log("Carregando Url");
                if (await youtubevideo.TrocarUrl(url))
                {
                    Log($"URL do YouTube carregada com sucesso. Video: {await youtubevideo.GetTitulo()}");
                    Log("Preparando as ferramentas necessarias para o processo.");
                    progressoBar(50);
                    Log("Iniciando o download do audio.");
                    string saidapath = Path.Combine(saidavideoyoutube.Text, $"{await youtubevideo.GetTitulo()}.mp4");
                    if (Path.Exists(saidapath))
                    {
                        Log("Ja existe um arquivo com o nome de saida, hora de renomear para um novo nome.");
                        saidapath = Path.Combine(saidavideoyoutube.Text, $"{DateTime.Now:HH_mm_ss_}{await youtubevideo.GetTitulo()}.mp4");
                    }
                    await youtubevideo.DownloadAudioLocal(saidapath);
                    progressoBar(80);
                    Log("Download completo.");
                    progressoBar(100);
                    progressoLabel("");
                }
                else
                {
                    progressoLabel("");
                    Log("Falha ao carregar a URL do YouTube.");
                    onprocess = false;
                }
                onprocess = false;
            }
            catch (Exception ex)
            {
                Log($"Ocorreu o seguinte erro: {ex.ToString()}");
                Log("Ocorreu um erro ao baixar o audio, verifique o link e a saida do audio");
                progressoLabel("");
                onprocess = false;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void combovideo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void qualidadebar_Scroll(object sender, EventArgs e)
        {

        }

        private void qualidadebar_Scroll_1(object sender, EventArgs e)
        {
            otimizarvalue.Text = qualidadebar.Value.ToString();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog
            {
                Description = "Selecione a pasta para salvar o video."
            };
            folderBrowserDialog.ShowDialog();
            if (!string.IsNullOrEmpty(folderBrowserDialog.SelectedPath))
            {
                saidatools.Text = folderBrowserDialog.SelectedPath;
            }
        }
    }
}
