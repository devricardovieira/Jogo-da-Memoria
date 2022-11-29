namespace Jogo_da_Memoria
{
    public partial class Form1 : Form
    {
        int movimentos, cliques, cartasEncontradas, tagIndex;

        Image[] img = new Image[8];

        List<string> lista = new List<string>();

        int[] tags = new int[2];

        public Form1()
        {
            InitializeComponent();
            Inicio();
        }

        private void Inicio() //nome do metodo
        {
            foreach(PictureBox item in Controls.OfType<PictureBox>()) //Todas as imagens colocadas com a imagem de verso atrás
            {
                tagIndex = int.Parse(String.Format("{0}",item.Tag)); 
                img[tagIndex] = item.Image; //chamei cada imagem e guardei dentro do vetor antes dela se tornar verso
                item.Image = Properties.Resources.verso;
                item.Enabled = true;
            }
            Posicoes();
        }

        private void Posicoes()
        {
            foreach (PictureBox item in Controls.OfType<PictureBox>())
            {
                Random rdn = new Random();
                int[] xP = { 33, 185, 337, 490 };
                int[] yp = { 33, 180, 325, 469 };

                Repete:
                var X = xP[rdn.Next(0, xP.Length)]; 
                var Y = yp[rdn.Next(0, yp.Length)];

                 

                string verificacao = X.ToString() + Y.ToString(); 

                if (lista.Contains(verificacao)) //verificar se as coordenadas já sairam. Se já, voltar ao repete.
                {
                    goto Repete;
                } 
                else
                {
                    item.Location = new Point(X, Y);
                    lista.Add(verificacao);
                }
            }
        }
        private void ImagensClick_Click(object sender, EventArgs e)
        {
            bool parEncontrado = false;


            PictureBox pic = (PictureBox)sender;
            cliques++;
            tagIndex = int.Parse(String.Format("{0}",pic.Tag));
            pic.Image = img[tagIndex];
            pic.Refresh();

            if(cliques == 1) //contador de cliques
            {
                tags[0] = int.Parse(String.Format("{0}", pic.Tag));
            }
            else if (cliques == 2) 
            {
                movimentos++;
                lblMovimentos.Text = "Movimentos : " + movimentos.ToString();
                tags[1] = int.Parse(String.Format("{0}", pic.Tag));
                parEncontrado = ChecagemPares();
                Desvirar(parEncontrado);
            }
        }
        private bool ChecagemPares()
        {
            cliques = 0;
            if (tags[0] == tags[1])
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void Desvirar(bool check)
        {
            Thread.Sleep(1000); //tempo para a carta espera para virar

            foreach (PictureBox item in Controls.OfType<PictureBox>())
            {
                if (int.Parse(String.Format("{0}", item.Tag)) == tags[0] || int.Parse(String.Format("{0}", item.Tag)) == tags[1])
                {
                    if (check == true)
                    {
                        item.Enabled = false;
                        cartasEncontradas++;
                    }
                    else
                    {
                        item.Image = Properties.Resources.verso;
                        item.Refresh();
                    }
                }
            }
            FinalJogo();
        }
        private void FinalJogo()
        {
            if (cartasEncontradas == (img.Length * 2))
            {
                MessageBox.Show("Parabéns, você terminou o jogo com " + movimentos.ToString() + " movimentos");
                DialogResult msg = MessageBox.Show("Deseja continuar o jogo ?", "Caixa de pergunta", MessageBoxButtons.YesNo);

                if (msg == DialogResult.Yes)
                {
                    cliques = 0; movimentos = 0; cartasEncontradas = 0;
                    lista.Clear();
                    Inicio();

                }
                else if (msg == DialogResult.No)
                {
                    MessageBox.Show("Obrigado por jogar!");
                    Application.Exit();
                }
            }
        }
    }
}