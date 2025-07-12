## Motivação e Resultado
  Conforme fui assistindo a vídeos no YouTube, percebi que baixar um vídeo é relativamente complicado. Por isso, decidi criar uma interface que me permitisse realizar essa tarefa com mais facilidade. Utilizando Windows Forms, integrei algumas bibliotecas públicas com outras que eu mesmo desenvolvi, a fim de implementar métodos para baixar vídeos do YouTube, otimizá-los em termos de tamanho do arquivo e extrair seus frames. Após algumas horas de trabalho, consegui alcançar meu objetivo: criar uma interface que me ajudasse a salvar, de forma otimizada, os vídeos que gosto no YouTube.
## O Programa e como usar
### Interface e Download de video:
#### 1. A interface foi desenvolvida de forma que as opções para baixar vídeos do YouTube ficassem separadas das opções de edição de vídeo. 
<img width="797" height="479" alt="image" src="https://github.com/user-attachments/assets/1d315675-daa7-47e2-a6d5-78b76b17025b" />

#### 2. Se você deseja baixar um vídeo, basta inserir o link na primeira opção, onde está escrito 'YouTube Link'. Em seguida, selecione o local onde o vídeo será salvo e escolha a opção desejada: baixar vídeo ou baixar áudio. 
<img width="798" height="478" alt="image" src="https://github.com/user-attachments/assets/d9f0cbda-1710-4d7c-998a-af49b07bf1d9" />

#### 3. Enquanto o programa estiver em execução, baixando ou otimizando o seu vídeo, ele exibirá as informações no console localizado abaixo. 
<img width="797" height="478" alt="image" src="https://github.com/user-attachments/assets/928df8e0-bd1c-4c4b-9f49-d60b293590ce" />

#### 4. Após baixar o vídeo, o programa irá informar você pelo console abaixo, e o vídeo será salvo na pasta que você selecionou. 
<img width="818" height="245" alt="image" src="https://github.com/user-attachments/assets/e4c8e85a-a981-4e53-9bf8-960a282d87e8" />

### Extrair frames e Otimizar videos.

#### 1. Primeiramente, para utilizar as opções de otimizar ou extrair frames, você precisa carregar o arquivo de vídeo desejado pelo botão e, em seguida, selecioná-lo na lista do combobox. 
<img width="798" height="477" alt="image" src="https://github.com/user-attachments/assets/5c6202dd-a593-4a52-a272-a6f9e11f0e6c" />

#### 2. Para que tudo ocorra corretamente, é necessário definir um local onde os arquivos modificados serão salvos. 
<img width="799" height="479" alt="image" src="https://github.com/user-attachments/assets/4d1e5b3e-9c09-4c2d-ac68-227a05c49bcc" />

#### 3. Após selecionar o vídeo, você pode escolher entre duas opções: extrair frames ou otimizar. Na primeira opção, 'Extrair Frames', o programa começará a extrair todos os frames do vídeo selecionado e irá colocá-los em um arquivo .zip no local de saída que você definiu. 
OBS: Dependendo do tamanho do video, o processo de extração pode demorar. 
<img width="798" height="479" alt="image" src="https://github.com/user-attachments/assets/71dfa23a-20df-4f1b-9f15-70e2eae2fbe7" />
<img width="516" height="434" alt="image" src="https://github.com/user-attachments/assets/ed0efb29-49dc-456f-b7ef-79ef7e2c3a17" />

#### 4. No processo de otimização, o vídeo será salvo no caminho que você definiu, com o tamanho reduzido e a qualidade ajustada conforme as configurações escolhidas.
Obs: Quanto maior o nível de otimização, menor será o tamanho do vídeo e também a sua qualidade.
<img width="793" height="478" alt="image" src="https://github.com/user-attachments/assets/992b39eb-a0da-4668-ab6a-795b3f21a909" />
No final você tera resultados parecidos com esses:
<br>
<img width="184" height="121" alt="image" src="https://github.com/user-attachments/assets/8cf03114-1c4b-41a0-bdac-eb8e694c9ff2" />
<br>
<img width="719" height="503" alt="image" src="https://github.com/user-attachments/assets/77b5bd39-bfed-4631-8e22-ee3381b6ce7f" />
<img width="1430" height="531" alt="image" src="https://github.com/user-attachments/assets/62b886ba-0db3-4d9c-b030-97ecbfe661c0" />

##### No final, é possível obter uma redução de tamanho bastante significativa, com uma perda de qualidade quase imperceptível.






