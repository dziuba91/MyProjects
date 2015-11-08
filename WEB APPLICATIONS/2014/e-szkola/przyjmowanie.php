<?php
function rekrutacja()
{
	$start = $_GET['strona'];
	$na_stronie = 2;
	echo '<div id="container">';
	if (isset ($_POST['przyjmij']))
	{
		$j=0;
		for ($i=0;$i<$na_stronie;$i++)
		{
			if (isset($_POST['uczen'.$i]))
			{
			$zapytanie = "update uczen set status=1 where uczen_ID='".$_POST['uczen'.$i]."'";
			mysql_query($zapytanie);
			$j++;
			}
		}
	
		echo '<h1>Przyjęto '.$j.' uczniów!</h1>';
	}
	
	if (isset ($_POST['przydziel']))
	{
		$j=0;
		if ($_POST['klasa'] != "puste")
		{
			$zapytanie = "SELECT klasa_ID, poziom, podklasa from szkola.klasa where klasa_ID=".$_POST['klasa']."";
			$wynik = mysql_query($zapytanie);	
			if ($row = mysql_fetch_row($wynik))
			{
				$klasa_id = $row[0];
				$klasa_nazwa = $row[1].''.$row[2];
			}
		
			for ($i=0;$i<$na_stronie;$i++)
			{
				if (isset($_POST['uczen'.$i]))
				{
					$zapytanie = "update uczen set klasa_ID=".$klasa_id.", status=2 where uczen_ID='".$_POST['uczen'.$i]."'";
					mysql_query($zapytanie);
					$j++;
				}
			}
			echo '<h1>Przydzielono '.$j.' uczniów do klasy '.$klasa_nazwa.'</h1>';
		}
		else
			echo '<h1>Nie wybrano klasy!</h1>';
	}
	
	if (isset ($_POST['wroc']))
	{
		$j=0;
	
		for ($i=0;$i<$na_stronie;$i++)
		{
			if (isset($_POST['uczen'.$i]))
			{
				$zapytanie = "update uczen set status=0, klasa_ID=NULL where uczen_ID='".$_POST['uczen'.$i]."'";
				mysql_query($zapytanie);
				$j++;
			}
		}
		
		echo '<h1>Cofnięto '.$j.' uczniów na etap oczekiwania!</h1>';
	}
	
	if (isset ($_POST['odrzuc']))
	{
		$j=0;
		for ($i=0;$i<$na_stronie;$i++)
		{
			if (isset($_POST['uczen'.$i]))
			{
				$zapytanie = "update uczen set status=3 where uczen_ID='".$_POST['uczen'.$i]."'";
				mysql_query($zapytanie);
				$j++;
			}
		}
		
		echo '<h1>Odrzucono podania '.$j.' uczniów!</h1>';
	}
	
	if (isset ($_POST['zakoncz']))
	{
		$zapytanie = "select uczen.profil_ID from uczen inner join profil on (uczen.profil_ID=profil.profil_ID) where status<2 or status=3";
		$wynik = mysql_query($zapytanie);	
		while ($row = mysql_fetch_row($wynik))
		{	
			$zapytanie = "delete from uczen where profil_id=".$row[0]."";
			mysql_query($zapytanie);
			$zapytanie = "delete from profil where profil_id=".$row[0]."";
			mysql_query($zapytanie);
		}
		$zapytanie = "select nazwisko, `imię_matki`, `imię_ojca`, uczen_ID, profil.profil_ID from uczen inner join profil on (uczen.profil_ID=profil.profil_ID) where status=2";
		$wynik = mysql_query($zapytanie);	
		while ($row = mysql_fetch_row($wynik))
		{	
			$login=$row[0].''.$row[1].''.$row[3];
			$login2=$row[0].''.$row[2].''.$row[3];
			$haslo=randomPassword();
			$haslo2=randomPassword();
			$uczen_id=$row[4];
			$zapytanie2 = "insert into profil (`login`, `haslo`, `prawa`, `imie`, `nazwisko`, `pesel`, `data_urodzenia`) values
				('".$login."', '".$haslo."', 'rodzic', '".$row[1]."', '".$row[0]."', '0', '1999-01-01')";
			$wynik2 = mysql_query($zapytanie2);
			$zapytanie2 = "insert into profil (`login`, `haslo`, `prawa`, `imie`, `nazwisko`, `pesel`, `data_urodzenia`) values
				('".$login2."', '".$haslo2."', 'rodzic', '".$row[2]."', '".$row[0]."', '0', '1999-01-01')";
			$wynik2 = mysql_query($zapytanie2);
			$zapytanie2 = "select profil_ID from profil where login='".$login."' or login='".$login2."'";
			$wynik2 = mysql_query($zapytanie2);
			while ($row2 = mysql_fetch_row($wynik2))
			{
				$zapytanie3 = "insert into uczen_rodzic (`rodzic_ID`, `uczen_ID`) values (".$row2[0].", ".$uczen_id.")";
				mysql_query($zapytanie3);
			}
	
			$temat="Informacja rekrutacyjna.";
			$tresc='Zostałeś przyjęty.\n\nUtworzono konta dla rodziców:\nlogin: '.$login.'\nhasło: '.$haslo.'\n\nlogin: '.$login2.'\nhasło: '.$haslo2.'';
			$zapytanie2="insert into wiadomosc (odbiorca_ID, nadawca_ID, tresc, temat, nowa) values 
				(".$uczen_id.", '".$_SESSION['id']."', '".$tresc."', '".$temat."', 1)";
			$wynik2 = mysql_query($zapytanie2);
		}
			
		$zapytanie = "update uczen set status=4 where status=2";
		mysql_query($zapytanie);
		echo '<h1>Zakończono rekrutację!</h1>';
	}
		
	echo '<form method="POST" action="funkcje.php?akcja=rekrutacja&strona=0">';
	echo ' 	<center>
			<select name="status" size="1" onchange="this.form.submit()">
			<option value="puste"></option>
			<option value="oczekujacy">Oczekujący</option>
			<option value="przyjeci">Przyjęci</option>
			<option value="przydzieleni">Przydzieleni</option>
			<option value="nprzyjeci">Nieprzyjęci</option>
			<option value="zakoncz">Zakończ</option>
			</select></center>';
	
	if (isset ($_POST['status']))
		$_SESSION['status']=$_POST['status'];
		
	if (isset($_SESSION['status']) && $_SESSION['status']=='oczekujacy')
	{
		echo '<h2>Uczniowie oczekujący na decyzję:</h2>';
		wypisz(0);
		echo '<center><input type="submit" value="Przyjmij" name="przyjmij"><input type="submit" value="Odrzuć" name="odrzuc"></center>';
	}
	
	if (isset($_SESSION['status']) && $_SESSION['status']=='przyjeci')
	{
		echo '<h2>Uczniowie oczekujący na przydział do klasy:</h2>';
		echo '<form method="POST" action="funkcje.php?akcja=rekrutacja&strona=0">';
		echo '<h1>Przydziel do klasy: ';
		echo '<select name="klasa" size="1">
			<option value="puste"></option>';
		$zapytanie = "SELECT klasa_ID, poziom, podklasa from szkola.klasa;";
		$wynik = mysql_query($zapytanie);		
		while ($row = mysql_fetch_row($wynik)) 
			echo "<option value=\"".$row[0]."\">".$row[1]."".$row[2]."</option>";
		echo '</select>';
		wypisz(1);
		echo '<center><input type="submit" value="Przydziel" name="przydziel"></center>';
	}
	
	if (isset($_SESSION['status']) && $_SESSION['status']=='przydzieleni')
	{
		echo '<h2>Uczniowie przydzieleni do klas:</h2>';
		wypisz(2);
		echo '<center><input type="submit" value="Wróc do oczekujących" name="wroc"></center>';
	}
	
	if (isset($_SESSION['status']) && $_SESSION['status']=='nprzyjeci')
	{
		echo '<h2>Uczniowie nieprzyjęci:</h2>';
		wypisz(3);
		echo '<center><input type="submit" value="Wróc do oczekujących" name="wroc"></center>';
	}
	
	echo '</form>';
	
	if (isset($_SESSION['status']) && $_SESSION['status']=='zakoncz')
	{
		echo '<form method="POST" onsubmit="return confirm(';
		echo "'Jesteś pewien swojej decyzji?'";
		echo ');" action="funkcje.php?akcja=rekrutacja&strona=0">';
		echo '<h2>UWAGA! PO ZATWIERDZENIU ZAKOŃCZENIA REKRUTACJI USUNIĘTE ZOSTANĄ WSZYSTKIE NIEPRZYDZIELONE DO KLAS KONTA UCZNIÓW!</h2>';
		echo '<center><input type="submit" value="POTWIERDŹ" name="zakoncz"></center></form>';
	}
}

function wypisz($x)
{
	$zapytanie = "select uczen_ID from uczen where status=".$x."";
	$ilosc= mysql_num_rows(mysql_query($zapytanie));
	$na_stronie = 2;
	if($ilosc>$na_stronie) 
	{
		echo '<center>Strona ';
		for($i=0; $i<ceil($ilosc/$na_stronie); $i++)
			echo '<a href="funkcje.php?akcja=rekrutacja&strona='.($i*$na_stronie).'">'.($i+1).'</a> | ';
		
		echo '</center></br>';
		
		if ($x==2)
			$zapytanie = "select nazwisko, imie, pesel, data_urodzenia, imię_matki, imię_ojca, uczen_ID, poziom, podklasa from uczen 
				inner join profil on (uczen.profil_ID=profil.profil_ID)
				inner join klasa on (uczen.klasa_ID=klasa.klasa_ID) where status=".$x." order by nazwisko asc
			LIMIT ".$start.", ".$na_stronie."";
		else
			$zapytanie = "select nazwisko, imie, pesel, data_urodzenia, imię_matki, imię_ojca, uczen_ID from uczen 
				inner join profil on (uczen.profil_ID=profil.profil_ID) where status=".$x." order by nazwisko asc
			LIMIT ".$start.", ".$na_stronie."";
	}
	else
	{
		if ($x==2)
			$zapytanie = "select nazwisko, imie, pesel, data_urodzenia, imię_matki, imię_ojca, uczen_ID, poziom, podklasa from uczen 
				inner join profil on (uczen.profil_ID=profil.profil_ID)
				inner join klasa on (uczen.klasa_ID=klasa.klasa_ID) where status=".$x." order by nazwisko asc";
		else
			$zapytanie = "select nazwisko, imie, pesel, data_urodzenia, imię_matki, imię_ojca, uczen_ID from uczen 
				inner join profil on (uczen.profil_ID=profil.profil_ID) where status=".$x." order by nazwisko asc";
	}
	
	echo '</br></br><table class="pusta">';
	echo '<tr class="tytul"><td >Imię i nazwisko</td>';
	
	if ($x==2)
		echo '<td>Klasa</td>';
		
	echo '<td>Pesel</td><td>Data urodzenia</td><td>Imię matki</td><td>Imię ojca</td><td></td></tr>';
	$i=0;
	
	$wynik = mysql_query($zapytanie);
	while ($row = mysql_fetch_row($wynik))
	{
		echo '<tr><td>'.$row[0].' '.$row[1].'</td>';
		
		if ($x==2)
			echo '<td>'.$row[7].''.$row[8].'</td>';
			
		echo '<td>'.$row[2].'</td><td>'.$row[3].'</td><td>'.$row[4].'</td><td>'.$row[5].'</td>';
		echo '<td><input type="checkbox" id="uczen'.$i.'" name="uczen'.$i.'" value="'.$row[6].'"></tr>';
		$i++;
	}
	
	echo '<tr></tr><tr><td></td><td></td><td></td><td></td>';
	
	if ($x==2)
		echo '<td></td>';
		
	echo '<td class="czas">Zaznacz wszystko:</td><td><input type="checkbox" id="wszystkie" name="wszystkie" 
		value="wszystkie" onclick="zaznacz('.$ilosc.');"></td></tr></table>';
}

function randomPassword() {
    $alphabet = "abcdefghijklmnopqrstuwxyzABCDEFGHIJKLMNOPQRSTUWXYZ0123456789";
    $alphaLength = strlen($alphabet) - 1;
	$pass='';
	
    for ($i = 0; $i < 8; $i++) {
        $n = rand(0, $alphaLength);
        $pass.= $alphabet[$n];
    }
	
    return ($pass);
}
?>