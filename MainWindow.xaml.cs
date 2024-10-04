using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace RectangleAnimation
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Az animáció indítása.
		/// </summary>
		/// <param name="sender">Az Indít nyomógomb.</param>
		/// <param name="e"></param>
		private void btIndít_Click(object sender, RoutedEventArgs e)
		{
			// Az animáció időtartama.
			Duration időtartam = TimeSpan.FromSeconds(2);

			// Az áttetszőséget változtató animáció objektum létrehozása.
			DoubleAnimation da = new DoubleAnimation();
			// Kezdőérték.
			da.From = 1;
			// Végső érték.
			da.To = 0.1;
			// Az animáció időtartama.
			da.Duration = időtartam;
			// Ha vége van, csinálja meg visszafele is.
			da.AutoReverse = true;
			// Értesítjük a felhasználót az animáció befejeződéséről egy üzenetablakkal.
			da.Completed += (a, b) => MessageBox.Show("Animáció vége!");
			// Animáció elindítása.
			rcNégyzet.BeginAnimation(Rectangle.OpacityProperty, da);

			// Az átalakítást leíró objektumok definiálása.
			// Forgatást leíró objektum. Az első paraméter a szög lenne, de itt nincs jelentősége, mert 
			// a szöget az animációs objektum pillanatnyi értéke fogja meghatározni.
			// A következő két paraméter a középpontot adja meg a vezérlő bal felső sarkához viszonyítva.
			RotateTransform rtForgatás =
				new RotateTransform(0, rcNégyzet.Width / 2, 
				rcNégyzet.Height / 2);
			// Átméretezést leíró objektum. Az első két paraméter az átméretezés arányát adná meg, 
			// de itt nincs jelentősége, mert értéküket az animációs objektumok pillanatnyi értéke fogja 
			// meghatározni. A következő két paraméter a középpontot adja meg a vezérlő bal felső sarkához 
			// viszonyítva.
			ScaleTransform stKicsinyítés =
				new ScaleTransform(0, 0, rcNégyzet.Width / 2, rcNégyzet.Width / 2);
			// Az átalakítást leíró objektumokat tároló objektum.
			TransformGroup tgÁtalakít = new TransformGroup();
			// A forgatást leíró objektumot hozzáadjuk a tárolóhoz.
			tgÁtalakít.Children.Add(rtForgatás);
			// Az átméretezést leíró objektumot hozzáadjuk a tárolóhoz.
			tgÁtalakít.Children.Add(stKicsinyítés);
			// Az átalakítást leíró objektumokat a négyzethez rendeljük.
			rcNégyzet.RenderTransform = tgÁtalakít;

			// Animációs objektum a forgatáshoz.
			DoubleAnimation dr = new DoubleAnimation();
			// Kezdőszög.
			dr.From = 0;
			// Végső szög.
			dr.To = 360;
			// Az animáció időtartama.
			dr.Duration = időtartam;
			// Ha vége van, csinálja meg visszafele is.
			dr.AutoReverse = true;
			// Animáció elindítása. Itt a forgatást leíró objektumra hívjuk meg az animációt.
			rtForgatás.BeginAnimation(RotateTransform.AngleProperty, dr);

			// Animációs objektum az átméretezéshez.
			DoubleAnimation ds = new DoubleAnimation();
			// Kezdő arányérték 1: eredeti méret
			ds.From = 1.0;
			// Végső arányérték 0.1: az eredeti méret 10%-a.
			ds.To = 0.1;
			// Az animáció időtartama.
			ds.Duration = időtartam;
			// Ha vége van, csinálja meg visszafele is.
			ds.AutoReverse = true;
			// Animáció elindítása. Itt az átméretezést leíró objektumra hívjuk meg az animációt
			// külön vízszintes és függőleges irányban, mivel két tulajdonságot kell változtatni.
			stKicsinyítés.BeginAnimation(ScaleTransform.ScaleXProperty, ds);
			stKicsinyítés.BeginAnimation(ScaleTransform.ScaleYProperty, ds);
		}
	}

}
