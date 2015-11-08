<?php
function plan_utworz()
{
	$i=0;
	$j=0;
	echo '<div id="container">';	
	echo"
		<form method=\"POST\" action=\"funkcje.php?akcja=uzyt_dodaj\">	
		Klasa:
			<select name=\"klasa\" size=\"1\" onchange=\"this.form.submit()\">
			<option value=\"puste\"></option>";
	
	$zapytanie = "SELECT klasa_ID, poziom, podklasa from szkola.klasa;";
	$wynik = mysql_query($zapytanie);		
	while ($row = mysql_fetch_row($wynik)) 
	{
		echo " <option value=\"".$row[0]."\">".$row[1]."".$row[2]."</option>";
	}
	echo"
			</select>
		</form> ";
	
	if (isset($_POST['klasa']))
	{
		$licz=0;
		$klasa_ID=$_POST['klasa'];
		$_SESSION['klasa_ID']=$klasa_ID;
		pokaz_mod();
	}
	
	$i=0;
	$j=0;
	if (isset($_POST['zatwierdz_plan_zajec']))				//ZATWIERDZ
	{
		$dzien;
		$godzina_lekcyjna;
		$klasa_ID=$_SESSION['klasa_ID'];

		do 
		{
			if($i==0) $godzina_lekcyjna=1;
			if($i==1) $godzina_lekcyjna=2;
			if($i==2) $godzina_lekcyjna=3;
			if($i==3) $godzina_lekcyjna=4;
			if($i==4) $godzina_lekcyjna=5;
			if($i==5) $godzina_lekcyjna=6;
			if($i==6) $godzina_lekcyjna=7;
			if($i==7) $godzina_lekcyjna=8;
	
			do
			{
				$ok=2;
				if($j==0) $dzien='Poniedzialek';
				if($j==1) $dzien='Wtorek';
				if($j==2) $dzien='Sroda';
				if($j==3) $dzien='Czwartek';
				if($j==4) $dzien='Piatek';
				if($_POST['sala'.$i.''.$j.'']!='puste' && $_POST['przedmiot'.$i.''.$j.'']!='puste')
				for ($k=1;$k<21;$k++)
				{
					if ($_POST['nauczyciel'.$i.''.$j.''.$k.''] != 'puste')
					$ok=1;
				}
				
				if($_POST['sala'.$i.''.$j.'']=='puste' && $_POST['przedmiot'.$i.''.$j.'']=='puste') //USUWANIE
				for ($k=1;$k<21;$k++)
				{
					if ($_POST['nauczyciel'.$i.''.$j.''.$k.''] == 'puste')
					$ok=0;
				}
				
				if ((isset($ok)) && ($ok==0))
				{
					$zapytanie= "delete from plan_lekcji 
						where klasa_ID=".$klasa_ID." and godzina_lekcyjna=".$godzina_lekcyjna." and dzien_tygodnia='".$dzien."';";
					$wynik = mysql_query($zapytanie);
				}
				
				if ((isset($ok)) && ($ok==1))
				{	
					$sala=$_POST['sala'.$i.''.$j.''];
					for ($k=1;$k<21;$k++)
					{
						if ($_POST['nauczyciel'.$i.''.$j.''.$k.''] != 'puste')
							$nauczyciel=$_POST['nauczyciel'.$i.''.$j.''.$k.''];
					}
					$przedmiot=$_POST['przedmiot'.$i.''.$j.''];
					$zapytanie = "SELECT * from plan_lekcji 
						where klasa_ID=".$klasa_ID." and godzina_lekcyjna=".$godzina_lekcyjna." and dzien_tygodnia='".$dzien."';";
					$wynik = mysql_query($zapytanie);		
					if($row = mysql_fetch_row($wynik))
					{
						$zapytanie= "update plan_lekcji set sala_ID=".$sala.", godzina_lekcyjna=".$godzina_lekcyjna.", nauczyciel_ID=".$nauczyciel.", 
							klasa_ID=".$klasa_ID.", przedmiot_ID=".$przedmiot.", dzien_tygodnia='".$dzien."' 
							where klasa_ID=".$klasa_ID." and godzina_lekcyjna=".$godzina_lekcyjna." and dzien_tygodnia='".$dzien."';";
					}
					else
					{
						$zapytanie= "Insert into szkola.plan_lekcji (sala_ID, godzina_lekcyjna, nauczyciel_ID, klasa_ID, przedmiot_ID, dzien_tygodnia) 
							values ($sala, $godzina_lekcyjna, $nauczyciel, $klasa_ID, $przedmiot, '$dzien' );";
					}
					$wynik = mysql_query($zapytanie);
					$zmodyfikowany=1;
				}
				
				$j++;
			}while($j<5);
		
			$j=0;
			echo"</tr>";
			$i++;
		}while($i < 8);
		
		if(isset($zmodyfikowany)) 
			echo '<script>alert("ZMODYFIKOWANO PLAN ZAJEC");</script>';

		if (!isset($zmodyfikowany))
			echo '<script>alert("NIE WPROWADZONO ŻADNYCH MODYFIKACJI");</script>';
	}
	
	echo '</div>';
}


function pokaz_mod()
{
	//echo '<script>window.alert("Uwaga, plan posiada już wpisane dane!");</script>';
	$i=0;
	$j=0;
	$jest=0;
	$klasa_ID=$_POST['klasa'];
	$zapytanie = "SELECT poziom, podklasa from szkola.klasa where klasa_ID=".$klasa_ID.";";
	$wynik = mysql_query($zapytanie);		
	while ($row = mysql_fetch_row($wynik)) 
	{
		$klasa=$row[0];
		$podklasa=$row[1];
	}
	
	echo"
		<form method=\"POST\" action=\"funkcje.php?akcja=uzyt_dodaj\">";
	echo"<table>";
	echo "<tr>";
	echo'<th> Klasa '.$klasa.''.$podklasa.'</th>';
	echo"<th> Poniedzialek</th>";
	echo"<th> Wtorek</th>";
	echo"<th> Sroda</th>";
	echo"<th> Czwartek</th>";
	echo"<th> Piatek</th>";
	echo"</tr>";

	do 
	{
		echo"<tr>";
	
		do
		{
			$dzien;
			if ($j==0) $dzien = 'Poniedzialek';
			if ($j==1) $dzien = 'Wtorek';
			if ($j==2) $dzien = 'Sroda';
			if ($j==3) $dzien = 'Czwartek';
			if ($j==4) $dzien = 'Piatek';
			$godz_lekc=$i+1;
			if($j==0) 
				echo"<th> Lekcja ".$godz_lekc."</th>";
			$zapytanie = "SELECT sala_ID, godzina_lekcyjna, nauczyciel_ID, przedmiot_ID, dzien_tygodnia from plan_lekcji 
				where klasa_ID=".$klasa_ID." and godzina_lekcyjna=".$godz_lekc." and dzien_tygodnia='".$dzien."';";
			$wynik = mysql_query($zapytanie);
			if ($row = mysql_fetch_row($wynik))
			{
				$przed=$row[3];
				$naucz=$row[2];
				$sala=$row[0];
				$jest=1;
			}
			echo"
				<td> 
					Przedmiot:</br>
					<select name=\"przedmiot".$i."".$j."\" size=\"1\">
					<option value=\"puste\"></option>";
			
			$zapytanie = "SELECT przedmiot_ID, nazwa_przedmiotu from szkola.przedmiot order by nazwa_przedmiotu asc;";
			$wynik = mysql_query($zapytanie);		
			while ($row = mysql_fetch_row($wynik)) 
			{
				if ($jest==1 && $przed==$row[0])
					echo '<option onclick="pokaz_nauczycieli('.$i.','.$j.','.$row[0].')" value="'.$row[0].'" selected>'.$row[1].'</option>';
				else
					echo '<option onclick="pokaz_nauczycieli('.$i.','.$j.','.$row[0].')" value="'.$row[0].'">'.$row[1].'</option>';
			}
			
			echo"
				</select>";
				
			echo'
				</br>Nauczyciel:</br>';
				
			for ($k=1;$k<21;$k++)
			{
				echo '<select style="display:none;" name="nauczyciel'.$i.''.$j.''.$k.'" id="nauczyciel'.$i.''.$j.''.$k.'" size="1">
					<option value="puste"></option>';
				
				$zapytanie = "SELECT  profil.profil_ID, nazwisko, imie from profil 
					inner join nauczyciel on (nauczyciel.profil_ID=profil.profil_ID) where prawa='nauczyciel' and przedmiot_ID=".$k."";
				$wynik = mysql_query($zapytanie);		
				while ($row = mysql_fetch_row($wynik)) 
				{
					if ($jest==1 && $naucz==$row[0])
					{
						echo " <option value=\"".$row[0]."\" selected id=\"opcja".$i."".$j."".$k."\">".$row[1]." ".$row[2]."</option>";
						echo '<script>pokaz_nauczycieli('.$i.','.$j.','.$przed.');</script>';
					}
					else
					echo " <option value=\"".$row[0]."\" id=\"opcja".$i."".$j."".$k."\">".$row[1]." ".$row[2]."</option>";
				}
				echo"
					</select>";
			}
			
			echo"
				</br>Sala: </br>
				<select name=\"sala".$i."".$j."\" size=\"1\">
				<option value=\"puste\"></option>";
				
			$zapytanie = "SELECT  sala_ID, numer_sali from szkola.sala;";
			$wynik = mysql_query($zapytanie);		
			while ($row = mysql_fetch_row($wynik)) 
			{
				if ($jest==1 && $sala==$row[0])
					echo " <option value=\"".$row[0]."\" selected>".$row[1]."</option>";
				else
					echo " <option value=\"".$row[0]."\">".$row[1]."</option>";
			}
			
			echo"
				</select>
			</td>";		
			
			$j++;
			$jest=0;
		}
		while($j<5);
		
		$j=0;
		echo"</tr>";
        $i++;
	} 
	while($i < 8) ;

	echo"
		</table><input type=\"submit\" value=\"Zatwierdz plan zajec\" name=\"zatwierdz_plan_zajec\">
		</form>"; 
}

function plan()
{
	$i=0;
	$j=0;
	echo '<div id="container">';	
	echo"
		<form method=\"POST\" action=\"funkcje.php?akcja=uzyt_usun\">	
		<table class=\"pusta\">
		<tr><td>Klasa:</td><td>Nauczyciel:</td><td>Sala:</td></tr>
		<tr><td>
			<select display: inline-block name=\"klasa2\" size=\"1\" onchange=\"this.form.submit()\">
			<option value=\"puste\"></option>";
	
	$zapytanie = "SELECT klasa_ID, poziom, podklasa from szkola.klasa;";
	$wynik = mysql_query($zapytanie);		
	while ($row = mysql_fetch_row($wynik)) 
	{
		echo " <option value=\"".$row[0]."\">".$row[1]."".$row[2]."</option>";
	}
	
	echo"
			</select>
		</td><td>
			<select display: inline-block name=\"naucz2\" size=\"1\" onchange=\"this.form.submit()\">
			<option value=\"puste\"></option>";
	
	$zapytanie = "SELECT profil.profil_ID, imie, nazwisko from profil where prawa='nauczyciel' order by nazwisko asc;";
	$wynik = mysql_query($zapytanie);		
	while ($row = mysql_fetch_row($wynik)) 
	{
		echo " <option value=\"".$row[0]."\">".$row[2]." ".$row[1]."</option>";
	}
	
	echo"
			</select>
		</td><td>
			<select display: inline-block name=\"sala2\" size=\"1\" onchange=\"this.form.submit()\">
			<option value=\"puste\"></option>";
	
	$zapytanie = "SELECT sala_ID, numer_sali from sala;";
	$wynik = mysql_query($zapytanie);		
	while ($row = mysql_fetch_row($wynik)) 
	{
		echo " <option value=\"".$row[0]."\">".$row[1]."</option>";
	}
	
	echo"
			</select>
		</td></tr></table>
		</form> ";

	if ((isset($_POST['klasa2'])) && $_POST['klasa2'] != 'puste')
		plan_pokaz($_POST['klasa2'], 'klasa');
	if ((isset($_POST['naucz2'])) && $_POST['naucz2'] != 'puste')
		plan_pokaz($_POST['naucz2'], 'naucz');
	if ((isset($_POST['sala2'])) && $_POST['sala2'] != 'puste')
		plan_pokaz($_POST['sala2'], 'sala');
	if ((isset($_GET['akcja'])) && (isset($_GET['id'])))
	{
		plan_pokaz($_GET['id'], $_GET['akcja']);
	}
		
	echo '</div>';
}

function plan_pokaz($plan, $co)
{
	$i=0;
	$j=0;
	echo'<table style="display:table;"id="tabela" onclick="pokaz(0);">';
	echo "<tr>";

	if ($co=='klasa')
	{
		$zapytanie = "SELECT poziom, podklasa from szkola.klasa where klasa_ID=".$plan.";";
		$wynik = mysql_query($zapytanie);		
		while ($row = mysql_fetch_row($wynik)) 
		{
			$klasa=$row[0];
			$podklasa=$row[1];
		}
		
		echo'<th>Klasa '.$klasa.''.$podklasa.'</th>';
	}
	
	if ($co=='naucz')
	{
		$zapytanie = "SELECT imie, nazwisko from nauczyciel inner join profil on 
			(nauczyciel.profil_ID=profil.profil_ID) where nauczyciel.profil_ID=".$plan.";";
		$wynik = mysql_query($zapytanie);		
		while ($row = mysql_fetch_row($wynik)) 
		{
			$im=$row[0];
			$naz=$row[1];
		}
		
		echo'<th>'.$im.' '.$naz.'</th>';
	}
	
	if ($co=='sala')
	{
		$zapytanie = "SELECT numer_sali from sala where sala_ID=".$plan.";";
		$wynik = mysql_query($zapytanie);		
		while ($row = mysql_fetch_row($wynik)) 
		{
			$s=$row[0];
		}
		
		echo'<th> Sala '.$s.'</th>';
	}
	
	echo"<th> Poniedzialek</th>";
	echo"<th> Wtorek</th>";
	echo"<th> Sroda</th>";
	echo"<th> Czwartek</th>";
	echo"<th> Piatek</th>";
	echo"</tr>";
	
	do 
	{
		echo"<tr>";
	
		do
		{
			$dzien;
			if ($j==0) $dzien = 'Poniedzialek';
			if ($j==1) $dzien = 'Wtorek';
			if ($j==2) $dzien = 'Sroda';
			if ($j==3) $dzien = 'Czwartek';
			if ($j==4) $dzien = 'Piatek';
			$godz_lekc=$i+1;
			if($j==0) 
				echo"<th> Lekcja ".$godz_lekc."</th>";
			if ($co=='klasa')
				$zapytanie = "SELECT numer_sali, godzina_lekcyjna, imie, nazwisko, nazwa_przedmiotu, 
					plan_lekcji.sala_ID, plan_lekcji.nauczyciel_ID, klasa.poziom, klasa.podklasa, plan_lekcji.klasa_ID 
					from plan_lekcji inner join nauczyciel on (nauczyciel.profil_ID=plan_lekcji.nauczyciel_ID) 
						inner join klasa on (klasa.klasa_ID=plan_lekcji.klasa_ID)
						inner join profil on (profil.profil_ID=nauczyciel.profil_ID) 
						inner join przedmiot on (przedmiot.przedmiot_ID=plan_lekcji.przedmiot_ID) 
						inner join sala on (sala.sala_ID=plan_lekcji.sala_ID)
					where plan_lekcji.klasa_ID=".$plan." and godzina_lekcyjna=".$godz_lekc." and dzien_tygodnia='".$dzien."';";
			if ($co=='sala')
				$zapytanie = "SELECT numer_sali, godzina_lekcyjna, imie, nazwisko, nazwa_przedmiotu, 
					plan_lekcji.sala_ID, plan_lekcji.nauczyciel_ID, klasa.poziom, klasa.podklasa, plan_lekcji.klasa_ID 
					from plan_lekcji inner join nauczyciel on (nauczyciel.profil_ID=plan_lekcji.nauczyciel_ID) 
						inner join klasa on (klasa.klasa_ID=plan_lekcji.klasa_ID)
						inner join profil on (profil.profil_ID=nauczyciel.profil_ID) 
						inner join przedmiot on (przedmiot.przedmiot_ID=plan_lekcji.przedmiot_ID) 
						inner join sala on (sala.sala_ID=plan_lekcji.sala_ID)
					where plan_lekcji.sala_ID=".$plan." and godzina_lekcyjna=".$godz_lekc." and dzien_tygodnia='".$dzien."';";
			if ($co=='naucz')
				$zapytanie = "SELECT numer_sali, godzina_lekcyjna, imie, nazwisko, nazwa_przedmiotu, 
					plan_lekcji.sala_ID, plan_lekcji.nauczyciel_ID, klasa.poziom, klasa.podklasa, plan_lekcji.klasa_ID 
					from plan_lekcji inner join nauczyciel on (nauczyciel.profil_ID=plan_lekcji.nauczyciel_ID) 
						inner join klasa on (klasa.klasa_ID=plan_lekcji.klasa_ID)
						inner join profil on (profil.profil_ID=nauczyciel.profil_ID) 
						inner join przedmiot on (przedmiot.przedmiot_ID=plan_lekcji.przedmiot_ID) 
						inner join sala on (sala.sala_ID=plan_lekcji.sala_ID)
					where plan_lekcji.nauczyciel_ID=".$plan." and godzina_lekcyjna=".$godz_lekc." and dzien_tygodnia='".$dzien."';";
			
			$wynik = mysql_query($zapytanie);
			if ($row = mysql_fetch_row($wynik))
			{
				echo '<td>';
				echo '<b><p2>'.$row[4].'</p2></b></br>';
				echo '<a href="funkcje.php?akcja=naucz&id='.$row[6].'">'.$row[2].' '.$row[3].'</a></br>';
				echo '<a href="funkcje.php?akcja=sala&id='.$row[5].'">Sala: '.$row[0].'</a></br></br>';
				echo '<a href="funkcje.php?akcja=klasa&id='.$row[9].'">Klasa: '.$row[7].''.$row[8].'</a>';
				echo '</td>';
			}
			else
				echo '<td></td>';
			$j++;
		}
		while($j<5);
		
		$j=0;
		echo"</tr>";
        $i++;
	} 
	while($i < 8);
	
	echo '</table>';
}


function pokaz_plan_uczen()
{
	echo '<div id="container">';

	echo'<table style="display:table;"id="tabela" onclick="pokaz(0);">';
	echo "<tr>";
	$id=$_SESSION['id'];
	$zapytanie = "SELECT klasa.klasa_ID, poziom, podklasa from klasa, uczen where profil_ID=".$id." AND uczen.klasa_ID=klasa.klasa_ID;";
	$wynik = mysql_query($zapytanie);		
	while($row = mysql_fetch_row($wynik))
	{
		$klasa_ID=$row[0];
		$poziom=$row[1];
		$podklasa=$row[2];
	}

	echo'<th>Klasa '.$poziom.''.$podklasa.'</th>';

	echo"<th> Poniedzialek</th>";
	echo"<th> Wtorek</th>";
	echo"<th> Sroda</th>";
	echo"<th> Czwartek</th>";
	echo"<th> Piatek</th>";
	echo"</tr>";
	$i=0;
	$j=0;

	do 
	{
		echo"<tr>";
	
		do
		{
			$dzien;
			if ($j==0) $dzien = 'Poniedzialek';
			if ($j==1) $dzien = 'Wtorek';
			if ($j==2) $dzien = 'Sroda';
			if ($j==3) $dzien = 'Czwartek';
			if ($j==4) $dzien = 'Piatek';
			$godz_lekc=$i+1;
			if($j==0) 
				echo"<th> Lekcja ".$godz_lekc."</th>";
		
			$zapytanie = "SELECT numer_sali, godzina_lekcyjna, imie, nazwisko, nazwa_przedmiotu, 
				plan_lekcji.sala_ID, plan_lekcji.nauczyciel_ID, klasa.poziom, klasa.podklasa, plan_lekcji.klasa_ID 
				from plan_lekcji inner join nauczyciel on (nauczyciel.profil_ID=plan_lekcji.nauczyciel_ID) 
					inner join klasa on (klasa.klasa_ID=plan_lekcji.klasa_ID)
					inner join profil on (profil.profil_ID=nauczyciel.profil_ID) 
					inner join przedmiot on (przedmiot.przedmiot_ID=plan_lekcji.przedmiot_ID) 
					inner join sala on (sala.sala_ID=plan_lekcji.sala_ID)
				where plan_lekcji.klasa_ID=".$klasa_ID." and godzina_lekcyjna=".$godz_lekc." and dzien_tygodnia='".$dzien."';";
			$wynik = mysql_query($zapytanie);
			if ($row = mysql_fetch_row($wynik))
			{
				echo '<td>';
				echo '<b><p2>'.$row[4].'</p2></b></br>';
				echo '<a href="funkcje.php?akcja=naucz&id='.$row[6].'">'.$row[2].' '.$row[3].'</a></br>';
				echo '<a href="funkcje.php?akcja=sala&id='.$row[5].'">Sala: '.$row[0].'</a></br></br>';
				echo '<a href="funkcje.php?akcja=klasa&id='.$row[9].'">Klasa: '.$row[7].''.$row[8].'</a>';
				echo '</td>';
			}
			else
				echo '<td></td>';
		
			$j++;
		}
		while($j<5);
		
		$j=0;
		echo"</tr>";
        $i++;
	} 
	while($i < 8);
	
	echo '</table>';
	echo '</div>';
}

function pokaz_plan_nauczyciel()
{
	echo '<div id="container">';	

	echo'<table style="display:table;"id="tabela" onclick="pokaz(0);">';
	echo "<tr>";
	
	$id=$_SESSION['id'];
	$zapytanie = "SELECT imie, nazwisko, profil_ID from  profil where profil.profil_ID=".$id.";";
	$wynik = mysql_query($zapytanie);		
	while($row = mysql_fetch_row($wynik))
	{
		$profil_nauczyciel_ID=$row[2];
		$imie=$row[0];
		$nazwisko=$row[1];
	}

	echo'<th>'.$nazwisko.''.$imie.'</th>';

	echo"<th> Poniedzialek</th>";
	echo"<th> Wtorek</th>";
	echo"<th> Sroda</th>";
	echo"<th> Czwartek</th>";
	echo"<th> Piatek</th>";
	echo"</tr>";
	$i=0;
	$j=0;
	
	do 
	{
		echo"<tr>";
	
		do
		{
			$dzien;
			if ($j==0) $dzien = 'Poniedzialek';
			if ($j==1) $dzien = 'Wtorek';
			if ($j==2) $dzien = 'Sroda';
			if ($j==3) $dzien = 'Czwartek';
			if ($j==4) $dzien = 'Piatek';
			$godz_lekc=$i+1;
			if($j==0) 
				echo"<th> Lekcja ".$godz_lekc."</th>";
		
			$zapytanie = "SELECT numer_sali, godzina_lekcyjna, imie, nazwisko, nazwa_przedmiotu, 
				plan_lekcji.sala_ID, plan_lekcji.nauczyciel_ID, klasa.poziom, klasa.podklasa, plan_lekcji.klasa_ID 
				from plan_lekcji inner join nauczyciel on (nauczyciel.profil_ID=plan_lekcji.nauczyciel_ID) 
					inner join klasa on (klasa.klasa_ID=plan_lekcji.klasa_ID)
					inner join profil on (profil.profil_ID=nauczyciel.profil_ID) 
					inner join przedmiot on (przedmiot.przedmiot_ID=plan_lekcji.przedmiot_ID) 
					inner join sala on (sala.sala_ID=plan_lekcji.sala_ID)
				where plan_lekcji.nauczyciel_ID=".$profil_nauczyciel_ID." and godzina_lekcyjna=".$godz_lekc." and dzien_tygodnia='".$dzien."';";
			$wynik = mysql_query($zapytanie);
			if ($row = mysql_fetch_row($wynik))
			{
				echo '<td>';
				echo '<b><p2>'.$row[4].'</p2></b></br>';
				echo '<a href="funkcje.php?akcja=naucz&id='.$row[6].'">'.$row[2].' '.$row[3].'</a></br>';
				echo '<a href="funkcje.php?akcja=sala&id='.$row[5].'">Sala: '.$row[0].'</a></br></br>';
				echo '<a href="funkcje.php?akcja=klasa&id='.$row[9].'">Klasa: '.$row[7].''.$row[8].'</a>';
				echo '</td>';
			}
			else
				echo '<td></td>';
		
			$j++;
		}
		while($j<5);
		
		$j=0;
		echo"</tr>";
        $i++;
	} 
	while($i < 8) ;

	echo '</table>';
	echo '</div>';
}

function plan_szkoly()
{
	echo '<div id="container">';	

	echo'<table style="display:table;"id="tabela" onclick="pokaz(0);">';
	echo "<tr>";
	echo"<th>Dzień</th>";
	echo"<th>Godzina lekcyjna</th>";

	$zapytanie_klasa = "select poziom, podklasa from klasa;";
	$wynik_klasa = mysql_query($zapytanie_klasa);		
	while($row = mysql_fetch_row($wynik_klasa))
	{
		$poziom=$row[0];
		$podklasa=$row[1];
		echo'<th>'.$poziom.''.$podklasa.'</th>';
	}
	
	$zapytanie_nauczyciel = "select imie, nazwisko, nauczyciel_ID from profil, nauczyciel where nauczyciel.profil_ID=profil.profil_ID;";
	$wynik_nauczyciel = mysql_query($zapytanie_nauczyciel);		
	while($row = mysql_fetch_row($wynik_nauczyciel))
	{
		$nazwisko=$row[1];
		$imie=$row[0];
		$nauczyciel_ID=$row[2];
		echo'<th>'.$nazwisko.' '.$imie.'</th>';
	}
		
	echo"</tr>";
	$i=0;
	$j=0;
	
	do 
	{
		$dzien;
		if ($i==0) $dzien = 'Poniedzialek';
		if ($i==1) $dzien = 'Wtorek';
		if ($i==2) $dzien = 'Sroda';
		if ($i==3) $dzien = 'Czwartek';
		if ($i==4) $dzien = 'Piatek';
		
		do
		{
			//
			echo"<tr>";
		
			$godz_lekc=$j+1;
			if($j==0)echo'<th rowspan=8>'.$dzien.'  </th>';
				echo'<th> Lekcja '.$godz_lekc.'</th>';
			
			$zapytanie_klasa = "select klasa_ID, poziom, podklasa from klasa;";
			$wynik_klasa = mysql_query($zapytanie_klasa);		
			while($row = mysql_fetch_row($wynik_klasa))
			{
				$klasa_ID=$row[0];
				$poziom=$row[1];
				$podklasa=$row[2];
				
				$zapytanie_przedmiot = "select nazwa_przedmiotu, numer_sali from plan_lekcji, przedmiot, sala where klasa_ID=".$klasa_ID." and godzina_lekcyjna=".$godz_lekc." and dzien_tygodnia='".$dzien."' and plan_lekcji.przedmiot_ID=przedmiot.przedmiot_ID and plan_lekcji.sala_ID=sala.sala_ID ";
				$wynik_przedmiot = mysql_query($zapytanie_przedmiot);
				if($row2 = mysql_fetch_row($wynik_przedmiot))
				{
					$przedmiot=$row2[0];
					$sala=$row2[1];
					echo'<td> '.$przedmiot.', '.$sala.'</td>';
				}
				else
				{
					echo'<td></td>';
					//echo'<td>'.$dzien.', '.$godz_lekc.', '.$klasa_ID.'</td>';
				}
			}	
		
			$zapytanie_nauczyciel = "select nauczyciel_ID, imie, nazwisko from profil, nauczyciel where nauczyciel.profil_ID=profil.profil_ID;";
			$wynik_nauczyciel = mysql_query($zapytanie_nauczyciel);		
			while($row = mysql_fetch_row($wynik_nauczyciel))
			{
				$nauczyciel_ID=$row[0]+1;
				$zapytanie_sala = "SELECT poziom, podklasa from plan_lekcji, klasa where nauczyciel_ID=".$nauczyciel_ID." and godzina_lekcyjna=".$godz_lekc." and dzien_tygodnia='".$dzien."' and klasa.klasa_ID = plan_lekcji.klasa_ID ";
				$wynik_sala = mysql_query($zapytanie_sala);
				if($row2 = mysql_fetch_row($wynik_sala))
				{
					$poziom=$row2[0];
					$podklasa=$row2[1];
					echo'<td> '.$poziom.''.$podklasa.'</td>';
				}
				else
				{
					echo'<td></td>';
					//echo'<td>'.$dzien.', '.$godz_lekc.', '.$klasa_ID.'</td>';
				}
			}
			
			$j++;
			echo"</tr>";
		}
		while($j<8);
		
		$j=0;
        $i++;
	} 
	while($i < 5) ;
	
	echo '</table>';
	echo '</div>';
}
?>