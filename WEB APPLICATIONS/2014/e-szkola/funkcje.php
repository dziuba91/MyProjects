<?php
include 'baza.php';
include 'strona_uzytkownika.php';
include 'plan.php';
include 'przyjmowanie.php';
include 'wiadomosci.php';
include 'zmiana.php';
include 'dziennik.php';
include 'dziennik_kreator_lekcji.php';
include 'zarzadzanie_uzytkownikami.php';

	if (isset($_POST['dodaj']))
		$_SESSION['action']="utworz_plan";
	if (isset($_POST['usun']))
		$_SESSION['action']="usun";
	if (isset($_POST['wyloguj']))				//================================== WYLOGUJ SIE ===================================
	{
		session_destroy();
		header("Location: index.php"); 
		}

	if (isset($_GET['akcja']) && isset($_SESSION['id'])) //AKCJA USTAWIONA I UZYTKOWNIK ZALOGOWANY
	{
	
		if (isset($_GET['id'])) //pokaz plan
		{
			echo '<div id="container">';
			plan_pokaz($_GET['id'], $_GET['akcja']);
		}
	
		if ($_GET['akcja']=='zmiana') //zmiana hasla/loginu
		{
			zmien_ustawienia();
		}
	
		if ($_GET['akcja']=='wiad_pokaz') //pokaz wiadomosci
		{
			wiadomosc_pokaz();
		}

		if ($_GET['akcja']=='wiad_nowa') //napisz wiadomosc
		{
			wiadomosc_napisz();
		
			if (isset($_POST['wyslij']))
				wiadomosc_wyslij();
		}

		if ($_GET['akcja']=='wiad_usun' && isset($_GET['wiad_id'])) //usun wiadomosc
		{
			$zapytanie = "select odbiorca_ID from wiadomosc where odbiorca_ID=".$_SESSION['id']."";
			$wynik = mysql_query($zapytanie);
			if ($row = mysql_fetch_row($wynik))
				wiadomosc_usun($_GET['wiad_id']);
			else
				echo '<h1>brak uprawnień</h1>';
		}
		
		if ($prawa=='administrator') // LINKI ADMINA
		{
			if ($_GET['akcja']=='nowy_user') //pokaz plan
			{
				add_user();
			}
	
			if ($_GET['akcja']=='strona_glowna') //strona glowna
			{
				// kod strony głównej
			}
	
			if ($_GET['akcja']=='usun_user') //pokaz plan
			{
				delete_user();
			}
		}
		
		if ($prawa=='planista') // LINKI ADMINA
		{
			if ($_GET['akcja']=='uzyt_usun') //pokaz plan
			{
				plan();
			}
	
			if ($_GET['akcja']=='uzyt_dodaj') //utworz plan
			{
				plan_utworz();
			}
	
			if ($_GET['akcja']=='strona_glowna') //strona glowna
			{
				// kod strony głównej
			}
		}

		if ($prawa=='dyrektor') // LINKI DYREKTORA
		{
			if ($_GET['akcja']=='rekrutacja') //pokaz plan
			{
				rekrutacja();
			}
	
			if ($_GET['akcja']=='strona_glowna') //strona glowna
			{
				// kod strony głównej
		
				utworz_lekcje();
			}
	
			if ($_GET['akcja']=='plan_szkoly') //pokaz plan szkoly
			{
				plan_szkoly();
			}
	
			if ($_GET['akcja']=='dziennik') //pokaz plan
			{
				dziennik();
			}
		}
		
		if($prawa=='uczen')
		{
			if ($_GET['akcja']=='pokaz_plan_uczen') //pokaz plan uczen
			{
				pokaz_plan_uczen();
			}
		}

		if($prawa=='nauczyciel')
		{
			if ($_GET['akcja']=='pokaz_plan_nauczyciel') //pokaz plan nauczyciel
			{
				pokaz_plan_nauczyciel();
			}
	
			if ($_GET['akcja']=='dziennik') //pokaz plan
			{
				dziennik();
			}
	
			if ($_GET['akcja']=='uzyt_usun') //pokaz plan
			{
				plan();
			}
	
			if ($_GET['akcja']=='strona_glowna') //strona glowna
			{
				// kod strony głównej
		
				utworz_lekcje();
			}
		}

		if ($prawa=='uczen') // LINKI UCZNIA
		{
			if ($_GET['akcja']=='strona_glowna' && isset($status))//strona glowna
			{
				echo '<div id="container">';
				if ($status<4)	echo '<h1>Aktualny status:</h1>';
				if ($status==0) echo '<h2>Oczekuje na decyzję...</h2>';
				if ($status==1) echo '<h2>Przyjęty, oczekuje na przydział do klasy...</h2>';
				if ($status==2)
				{
					$zapytanie = "select poziom, podklasa from klasa inner join uczen on (uczen.klasa_ID=klasa.klasa_ID) where uczen.profil_ID=".$_SESSION['id']."";
					$wynik = mysql_query($zapytanie);
					if ($row = mysql_fetch_row($wynik))
						echo '<h2>Przydzielony do klasy '.$row[0].''.$row[1].'</h2>';
				}		
				if ($status==3) echo '<h2>Nieprzyjęty.</h2>';
				if ($status==4)
				{
					$zapytanie = "select klasa_ID from uczen where uczen.profil_ID=".$_SESSION['id']."";
					$wynik = mysql_query($zapytanie);
					if ($row = mysql_fetch_row($wynik))
					{
						plan_pokaz($row[0], 'klasa');
					}
				}
			}
		}
		
		if ($prawa=='rodzic') // LINKI RODZICA	
		{
			if ($_GET['akcja']=='strona_glowna')//strona glowna
			{
				echo '<div id="container">';
				$zapytanie = "select klasa_ID from uczen inner join uczen_rodzic on (uczen_rodzic.uczen_ID=uczen.profil_ID)where uczen_rodzic.rodzic_ID=".$_SESSION['id']."";
				$wynik = mysql_query($zapytanie);
				while ($row = mysql_fetch_row($wynik))
				{
					plan_pokaz($row[0], 'klasa');
					echo 'ddd';
				}
			}
		}
	}
	
	echo '</div></body></html>';
?>