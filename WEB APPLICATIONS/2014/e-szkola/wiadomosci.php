<?php

function wiadomosc_pokaz()
{	
	$id=$_SESSION['id'];
	echo'<div id="container">';
	
	$zapytanie = "SELECT wiadomosc_ID FROM wiadomosc 
	inner join profil on (profil.profil_ID=wiadomosc.nadawca_ID) where wiadomosc.odbiorca_ID=".$id."";
	$ilosc= mysql_num_rows(mysql_query($zapytanie));
	$start = $_GET['strona'];
	$na_stronie = 5;	
	
	if($ilosc>$na_stronie) 
	{
		echo '<center>Strona ';
		for($i=0; $i<ceil($ilosc/$na_stronie); $i++)
			echo '<a href="funkcje.php?akcja=wiad_pokaz&strona='.($i*$na_stronie).'">'.($i+1).'</a> | ';
			echo '</center></br>';
		$zapytanie = "SELECT imie, nazwisko, temat, tresc, czas, profil_ID, wiadomosc_ID, nowa, prawa FROM wiadomosc 
			inner join profil on (profil.profil_ID=wiadomosc.nadawca_ID) where wiadomosc.odbiorca_ID=".$id." order by czas desc
			LIMIT ".$start.", ".$na_stronie.";";
	}
	else
	{
		$zapytanie = "SELECT imie, nazwisko, temat, tresc, czas, profil_ID, wiadomosc_ID, nowa, prawa FROM wiadomosc 
			inner join profil on (profil.profil_ID=wiadomosc.nadawca_ID) where wiadomosc.odbiorca_ID=".$id." order by czas desc";
	}
	
	$wynik = mysql_query($zapytanie);
	if (mysql_num_rows($wynik) > 0)
	{
		echo '<center><table class="wiad">';
		while ($row = mysql_fetch_row($wynik))
		{	
			$row[3]=ereg_replace("(\r|\n)", "<br />", $row[3]);
			if ($row[8]=='nauczyciel')
				$styl="tytul_n";
			else
				$styl="tytul";
				
			echo '<tr class="pusta"><td class="czas">'.$row[4].'</td><td class="'.$styl.'">'.$row[2].'</td>';
			echo '<td class="usun"><a onclick="return confirm';
			echo "('Skasować?')";
			echo '" href="funkcje.php?akcja=wiad_usun&wiad_id='.$row[6].'">Usuń</a></td></tr>';
			echo '<tr class="pusta"><td class="od">';
			echo '<a href="funkcje.php?akcja=wiad_nowa&profil_id='.$row[5].'">'.$row[1].' '.$row[0].'</a></td>';
			echo '<td class="tresc">'.$row[3].'</td><td class="nowa">';
			
			if ($row[7]==1) echo 'Nowa!';
			if ($row[7]==3) echo 'UWAGA!';
			
			echo '</td></tr>';
			echo '<tr class="przerwa"></tr>';
		}
		echo '</table></center>';
		
		$zapytanie = "update wiadomosc set nowa=0 where nowa=1 and wiadomosc.odbiorca_ID=".$id.";";
		$wynik = mysql_query($zapytanie);
		if($ilosc>$na_stronie) 
		{
			echo '<center>Strona ';
			for($i=0; $i<ceil($ilosc/$na_stronie); $i++)
				echo '<a href="funkcje.php?akcja=wiad_pokaz&strona='.($i*$na_stronie).'">'.($i+1).'</a> | ';
			
			echo '</center>';
		}
	}
	else
	echo '<h1>Brak wiadomości...</h1>';
	echo '</div>';
}

function wiadomosc_napisz()
{
	?>
	
	<div id="container">
	<div id="container_left">
		<form method="POST" action="funkcje.php?akcja=wiad_nowa">
			<h1>Wyszukiwarka użytkowników:</h1></br>
			<center><input type="text" name="ciag">
			<input type="submit" value="Szukaj" name="szukaj">
		</form>
		</br></br></br>
		
<?php
	if (isset($_POST['szukaj']))
		szukaj();
	?>
	
	</div>
		<form method="POST" action="funkcje.php?akcja=wiad_nowa">
			<center>
			<table class="pusta2">
			
<?php 
	if ($_SESSION['prawa'] == 'dyrektor' || $_SESSION['prawa'] == 'administrator')
		$prawa=0;
	else if ($_SESSION['prawa'] == 'nauczyciel')
		$prawa=1;
	else
		$prawa=2;
	if ($prawa <2) { 
	?>	
	
		<tr><td>
			<input type="radio" name="x" value="osoba" id="osoba" onclick="zmien('', '')"/>
			<label class="label1">Jedna osoba:</label>
		<td><input type="radio" name="x" value="klasa" id="klasa" onclick="pokaz('klasa_wiad')"/>
			<label class="label1">Do całej klasy:</label></td>
		<td><input type="radio" name="x" value="naucz" id="naucz" onclick="zmien('nauczyciele', 'nauczyciele')"/>
			<label class="label1">Do nauczycieli:</label></td><td>

<?php } if ($prawa <1) { ?>

			<input type="radio" name="x" value="wszyscy" id="wszyscy" onclick="zmien('wszyscy', 'wszyscy')"/>
			<label class="label1">Do wszystkich:</label> <?php } ?>

<?php if ($prawa == 1) { ?>
	
			<input type="radio" name="x" value="uwaga" id="uwaga" onclick="zmien('', '')"/>
			<label class="label1">Napisz uwagę:</label>

<?php } ?>
	
		</td></tr>
		<tr><td></td><td>

<?php
	echo "<select style=\"display:none;\" name=\"klasa_wiad\" id=\"klasa_wiad\" size=\"1\">
		<option value=\"puste\"></option>";
	
	$zapytanie = "SELECT klasa_ID, poziom, podklasa from szkola.klasa;";
	$wynik = mysql_query($zapytanie);		
	while ($row = mysql_fetch_row($wynik)) 
		echo " <option onclick=\"dodaj('klasa', '".$row[1]."".$row[2]."', '".$row[0]."')\" value=\"".$row[0]."\">".$row[1]."".$row[2]."</option>";
	
	echo"</select>";
	?>
	
		</td><td></td><td></td></tr>
	</table>
	
	<table class="pusta2">
		<tr><td>Do:</td><td>

<?php 
	if (isset($_GET['profil_id']))
	{
		$zapytanie = "select profil_ID, imie, nazwisko from profil where profil_ID=".$_GET['profil_id']."";
		$wynik = mysql_query($zapytanie);
		if ($row = mysql_fetch_row($wynik))
		{
			echo '<input class="temat" readonly type="text" name="do" id="do" value="'.$row[2].' '.$row[1].'">';
			echo '<input style="display:none;" type="text" name="do_id" id="do_id" value="'.$row[0].'">';
			echo '<input style="display:none;" type="text" name="czy_klasa" id="czy_klasa">';
		}
		else
		{
			echo '<input class="temat" readonly type="text" name="do" id="do">';
			echo '<input style="display:none;" type="text" name="do_id" id="do_id">';
			echo '<input style="display:none;" type="text" name="czy_klasa" id="czy_klasa">';
		}
	}
	else
	{
		echo '<input class="temat" readonly type="text" name="do" id="do">';
		echo '<input style="display:none;" type="text" name="do_id" id="do_id">';
		echo '<input style="display:none;" type="text" name="czy_klasa" id="czy_klasa">';
	}
	?>
	
		</td></tr>
		<tr><td>Temat:</td><td>
			<input maxlength="100" class="temat" type="text" name="temat">
		</td></tr><tr><td>Treść:</td><td>
			<textarea name="tresc"></textarea>
		</td></tr><tr><td></td>
		<td class="right"><input type="submit" value="Wyślij" name="wyslij"></td></tr></table>
			</center>
		</form>
	</div>
	
<?php
}

function szukaj()
{
	if (isset($_POST['ciag']))
		$szukaj=$_POST['ciag'];
	
	$zapytanie="select imie, nazwisko, profil_ID from profil where imie like '%".$szukaj."%' or nazwisko like '%".$szukaj."%' order by nazwisko asc";
	$wynik = mysql_query($zapytanie);
	
	echo '<table class="pusta">';
	$i=0;
	while ($row = mysql_fetch_row($wynik))
	{
		echo '<tr><td>'.$row[1].' '.$row[0];
		echo '<td><a href="#" onclick="dodaj';
		echo "('".$row[1]."','".$row[0]."',".$row[2].")";
		echo '">dodaj...</a></td></tr>';
	}
	
	echo '</table>';
}

function wiadomosc_wyslij()
{
	if (isset($_POST['do']) && isset($_POST['temat']) && isset($_POST['tresc']) && isset($_POST['do_id']))
	{
		$do = $_POST['do'];
		$id = $_POST['do_id'];
		$temat = $_POST['temat'];
		$tresc = $_POST['tresc'];
		$czy_klasa;
		if (isset($_POST['czy_klasa']) && $_POST['czy_klasa']=='klasa')
			$czy_klasa=1;
		if ($do != '' && $temat != '' && $tresc != '' && $id != '')
		{
			if ($czy_klasa==1) //do całej klasy, trzeba najpierw zaktualizowac baze, zeby napisac kod 
			{
				$zapytanie="select uczen.profil_ID from uczen inner join profil on(profil.profil_id=uczen.profil_id) where klasa_id=".$id.""; //edit
				$wynik = mysql_query($zapytanie);
				while ($row = mysql_fetch_row($wynik))
				{
					$zapytanie="insert into wiadomosc (odbiorca_ID, nadawca_ID, tresc, temat, nowa) values 
						(".$row[0].", '".$_SESSION['id']."', '".$tresc."', '".$temat."', 1)";
					$wynik2 = mysql_query($zapytanie);
				}
				echo '<script>alert("Wysłano wiadomość!");</script>';
			}
			else if ($do=='nauczyciele') //do wszystkich nauczycieli
			{
				$zapytanie="select profil_ID from profil where prawa='nauczyciel'";
				$wynik = mysql_query($zapytanie);
				while ($row = mysql_fetch_row($wynik))
				{
					$zapytanie="insert into wiadomosc (odbiorca_ID, nadawca_ID, tresc, temat, nowa) values 
						(".$row[0].", '".$_SESSION['id']."', '".$tresc."', '".$temat."', 1)";
					$wynik2 = mysql_query($zapytanie);
				}
				echo '<script>alert("Wysłano wiadomość!");</script>';
			}
			else if ($do=='wszyscy') //do wszystkich
			{
				$zapytanie="select profil_ID from profil";
				$wynik = mysql_query($zapytanie);
				while ($row = mysql_fetch_row($wynik))
				{
					$zapytanie="insert into wiadomosc (odbiorca_ID, nadawca_ID, tresc, temat, nowa) values 
						(".$row[0].", '".$_SESSION['id']."', '".$tresc."', '".$temat."', 1)";
					$wynik2 = mysql_query($zapytanie);
				}
				echo '<script>alert("Wysłano wiadomość!");</script>';
			}
			else if ($_POST['x'] == 'uwaga')
			{
				$zapytanie="select uczen_ID, rodzic_ID from uczen_rodzic where uczen_ID=".$id."";
				$wynik = mysql_query($zapytanie);
				while ($row = mysql_fetch_row($wynik))
				{
					$uid=$row[0];
					$rid=$row[1];
					$zapytanie2="insert into wiadomosc (odbiorca_ID, nadawca_ID, tresc, temat, nowa) values 
						(".$rid.", '".$_SESSION['id']."', '".$tresc."', '".$temat."', 3)";
					$wynik2 = mysql_query($zapytanie2);
				}
					$zapytanie2="insert into wiadomosc (odbiorca_ID, nadawca_ID, tresc, temat, nowa) values 
						(".$uid.", '".$_SESSION['id']."', '".$tresc."', '".$temat."', 3)";
					$wynik2 = mysql_query($zapytanie2);
					echo '<script>alert("Wysłano wiadomość!");</script>';
			}
			else
			{
				$zapytanie="select profil_ID from profil where profil_ID=".$id."";
				$wynik = mysql_query($zapytanie);
				if (mysql_num_rows($wynik) == 1)
				{
					$zapytanie="insert into wiadomosc (odbiorca_ID, nadawca_ID, tresc, temat, nowa) values 
						(".$id.", '".$_SESSION['id']."', '".$tresc."', '".$temat."', 1)";
					$wynik = mysql_query($zapytanie);
					echo '<script>alert("Wysłano wiadomość!");</script>';
				}
			}
		}
		else
			echo '<script>alert("BŁĄD");</script>';
	}
	else
		echo '<script>alert("BŁĄD");</script>';
}

function wiadomosc_usun($id)
{
	$zapytanie = "delete from wiadomosc where wiadomosc_ID=".$id."";
	$wynik = mysql_query($zapytanie);
	header("Location: funkcje.php?akcja=wiad&strona=0");
}
?>