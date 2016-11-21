<?php
	session_start();
?>

<html>
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
		<script type="text/javascript" src="skrypty.js"></script>
		<link rel="stylesheet" type="text/css" href="style.css" />
	</head>

<?php	
	if(isset($_SESSION['id']))	//================================== UZYTKOWNIK POPRAWNIE ZALOGOWANY ===============
	{
		$id=$_SESSION['id'];
		
		include 'baza.php';		//==================================== POBRANIE DANYCH UZYTKOWNIKA ==================
		
		echo '<div id="menu_top">';
		echo '<div id="message_top">';
		
		$zapytanie = "SELECT nowa from wiadomosc where nowa=1 and odbiorca_ID=".$id.";";
		$wynik = mysql_query($zapytanie);
		$ile = mysql_num_rows($wynik);
		if ($ile==1)
			$text="nowa";
		else if ($ile==2 || $ile==3 || $ile==4)
			$text="nowe";
		else
			$text="nowych";
		
		echo '<h1>Wiadomości <a href="funkcje.php?akcja=wiad_pokaz&strona=0">('.$ile.' '.$text.')</a></h1>';
		echo '<a href="funkcje.php?akcja=wiad_nowa">Napisz nową...</a>';
		echo '</div>';
		echo '<div id="login_top">';
		echo '<form method="POST" action="funkcje.php">';
		
		$zapytanie = "SELECT login, prawa, imie, nazwisko FROM szkola.profil where profil_ID='".$id."';";
		$wynik = mysql_query($zapytanie);
		while ($row = mysql_fetch_row($wynik)) 
		{
			echo '<h1>Witaj, <a href="funkcje.php?akcja=zmiana">'.$row[2].'</a>!</h1>';
			$prawa=$row[1];
			$_SESSION['prawa']=$prawa;
		}
		echo " <input type=\"submit\" value=\"Wyloguj\" name=\"wyloguj\">
				</form> </div>";
		echo '<h1>Menu użytkownika:</h1>';
		echo'<a href=funkcje.php?akcja=strona_glowna>Strona główna</a> || ';
		
		if($prawa==="administrator")			//==================================== PRAWA ZALOGOWANEGO UZYTKOWNIKA ================
		{
			echo'<a href=funkcje.php?akcja=nowy_user> Dodaj użytkownika</a> || ';
			echo'<a href=funkcje.php?akcja=usun_user>Usuń użytkownika</a>';
		}
		
		if($prawa==="planista")			//==================================== PRAWA ZALOGOWANEGO UZYTKOWNIKA ================
		{
			echo'<a href=funkcje.php?akcja=uzyt_dodaj>Utwóz / Modyfikuj plan zajęć</a> || ';
			echo'<a href=funkcje.php?akcja=uzyt_usun>Przeglądaj plany zajęć</a>';
		}
		
		if($prawa==="dyrektor")
		{
			echo'<a href=funkcje.php?akcja=plan_szkoly>Plan zajęć całej szkoły</a> || ';
			echo'<a href=funkcje.php?akcja=rekrutacja&strona=0>Rekrutacja</a> || ';
			echo'<a href=funkcje.php?akcja=dziennik>Edytuj Dziennik</a>';
		}
		
		if($prawa==="nauczyciel")
		{
			echo'<a href=funkcje.php?akcja=pokaz_plan_nauczyciel>Plan zajęć</a> || ';
			echo'<a href=funkcje.php?akcja=uzyt_usun>Przeglądaj plany zajęć</a> || ';
			echo'<a href=funkcje.php?akcja=dziennik>Edytuj Dziennik</a>';
		}
		
		if($prawa==="uczen")
		{
			$zapytanie = "select status from uczen where profil_ID=".$_SESSION['id']."";
			$wynik = mysql_query($zapytanie);
			if ($row = mysql_fetch_row($wynik))
				$status = $row[0];
			if ($status==4)
			{
				echo'<a href=funkcje.php?akcja=pokaz_plan_uczen>Pokaż mój plan zajęć</a> || '; //jeśli się pojawia błąd to zakomentuj 
																			// ale odkąd wysałeś mi nową wersje pojawia się błąd dotyczący zapytania do bazy danych.
				echo'<a href=funkcje.php?akcja=obecnosc>Sprawdź Obecność/ Oceny</a>';
			}
		}
		
		echo '</div>';
	}
	else				//======================================= UZYTKOWNIK SIE NIE ZALOGOWAL TYLKO WSZEDL NA PODSTRONE =======
	{
		session_destroy();
		header("Location: index.php");
	}
?>