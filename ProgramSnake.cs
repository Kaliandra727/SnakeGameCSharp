using SnakeGame;

const int largTela = 69;

const int altTela = 29;

const string caracterecobra = "■";

string[,] tela = new string[largTela, altTela];
bool jogoRodando = true;
List<Coordenada> coordenadascobra = new();
Direcao direcao = Direcao.Direita;
int placar = 0;
Random rand = new();

iniciarJogo();

void iniciarJogo()
{
    CriarCobra();
    CriarObstaculo();
    CriarObstaculo();
    CriarObstaculo();
    CriarObstaculo();
    CriarComida();
    CriarComida();
    CriarComida();
    LerTeclas();

    while (jogoRodando)
    {
        Thread.Sleep(50);
        TransladarCobra();
        Renderizar();
    }
    FimDeJogo();

}

void FimDeJogo()
{
    Console.Clear();
    Console.WriteLine("Fim de Jogo, Pontuação: " + placar);
}

void Renderizar()
{
    Console.Clear();
    var telaASerRenderizada = "";

    for (int a = 0; a < altTela; a++)
    {
        for (int l = 0; l < largTela; l++)
        {
            if(tela[l, a] is not null or " ")
            {
                telaASerRenderizada += tela[l, a];
            }
            else 
            {
                telaASerRenderizada += " ";
            }
        }
        telaASerRenderizada += "\n";
    }
    Console.WriteLine(telaASerRenderizada);
}

void TransladarCobra()
{
    var cabeca = coordenadascobra[0];
    var coordenadaRaboX = coordenadascobra[^1].x;
    var coordenadaRaboY = coordenadascobra[^1].y;

    for (int i = coordenadascobra.Count - 1; i > 0; i--)
    {
        coordenadascobra[i].x = coordenadascobra[i - 1].x;
        coordenadascobra[i].y = coordenadascobra[i - 1].y;
    }

    if (direcao is Direcao.Direita)
    {
        cabeca.x++;

        if(cabeca.x > largTela - 1)
        {
            cabeca.x = 0;
        }
    }


    if (direcao is Direcao.Esquerda)
    {
        cabeca.x--;

        if (cabeca.x < 0)
        {
            cabeca.x = largTela -1;
        }
    }


    if (direcao is Direcao.Cima)
    {
        cabeca.y--;

        if (cabeca.y < 0)
        {
            cabeca.y = altTela - 1;
        }
    }

    if (direcao is Direcao.Baixo)
    {
        cabeca.y++;

        if (cabeca.y > altTela - 1)
        {
            cabeca.y = 0;
        }
    }

    if(tela[cabeca.x, cabeca.y] == "*")
    {
        placar += rand.Next(1, 10);
        coordenadascobra.Add(new Coordenada(coordenadaRaboX, coordenadaRaboY));
        CriarComida();
    }

    if (tela[cabeca.x, cabeca.y] == caracterecobra)
    {
        jogoRodando = false;
        return;
    }

    if (tela[cabeca.x, cabeca.y] == "||")
    {
        jogoRodando = false;
        return;
    }

    AtualizarPosicaoCobra();

}

void LerTeclas()
{
    Thread task = new(LerAcaoDaTecla);
    task.Start();
}

void LerAcaoDaTecla()
{
    while (jogoRodando)
    {
        var tecla = Console.ReadKey();

        if (tecla.Key is ConsoleKey.UpArrow && direcao is not Direcao.Baixo)
        {
            direcao = Direcao.Cima;
        }

        if (tecla.Key is ConsoleKey.DownArrow && direcao is not Direcao.Cima)
        {
            direcao = Direcao.Baixo;
        }

        if (tecla.Key is ConsoleKey.LeftArrow && direcao is not Direcao.Direita)
        {
            direcao = Direcao.Esquerda;
        }

        if (tecla.Key is ConsoleKey.RightArrow && direcao is not Direcao.Esquerda)
        {
            direcao = Direcao.Direita;
        }
    }
}

void CriarComida()
{
   int aleatorioX,aleatorioY;

    do
    {
        aleatorioX =  rand.Next(0, largTela);
        aleatorioY = rand.Next(0, altTela);
    } while (tela[aleatorioX, aleatorioY] is not null or " ");

    tela[aleatorioX, aleatorioY] = "*";

}

void CriarObstaculo()
{
    int aleatorioA, aleatorioB;
    do
    {
        aleatorioA = rand.Next(0, largTela);
        aleatorioB = rand.Next(0, altTela);
    } while (tela[aleatorioA, aleatorioB] is not null or " ");

    tela[aleatorioA, aleatorioB] = "||";

}

void CriarCobra()
{
    coordenadascobra.Add(new Coordenada(9, 14));
    coordenadascobra.Add(new Coordenada(8, 14));
    coordenadascobra.Add(new Coordenada(7, 14));

    AtualizarPosicaoCobra();
}

void AtualizarPosicaoCobra()
{
    for (int l = 0; l < largTela ; l++)
    {
        for (int a = 0; a < altTela ; a++)
        {
            var posicaoDeveConterCobra = coordenadascobra.Any(coordenada => coordenada.x == l 
            && coordenada.y == a);

            if (posicaoDeveConterCobra)
            {
                tela[l, a] = caracterecobra;
                continue;
            }

            if (tela[l, a] == caracterecobra)
            {
                tela[l, a] = " ";
            }
        }
    }
}
