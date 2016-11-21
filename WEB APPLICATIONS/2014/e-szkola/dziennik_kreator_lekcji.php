<?php

function utworz_lekcje()
{
$godz_l_S=array(8,8,9,10,11,12,13,14);
$min_l_S=array(0,55,50,45,40,35,30,25);

$godz_l_E=array(8,9,10,11,12,13,14,15);
$min_l_E=array(45,40,35,30,25,20,15,10);

$czas_E=array($godz_l_E[0]+$min_l_E[0]/100,
			  $godz_l_E[1]+$min_l_E[1]/100,
			  $godz_l_E[2]+$min_l_E[2]/100,
			  $godz_l_E[3]+$min_l_E[3]/100,
			  $godz_l_E[4]+$min_l_E[4]/100,
			  $godz_l_E[5]+$min_l_E[5]/100,
			  $godz_l_E[6]+$min_l_E[6]/100,
			  $godz_l_E[7]+$min_l_E[7]/100);
			  
$dzien_tyg=array(
		"Poniedzialek"	=> 1,
		"Wtorek"		=> 2,
		"Sroda"			=> 3,
		"Czwartek"		=> 4,
		"Piatek"		=> 5,
		"Sobota"		=> 6,
		"Niedziela"		=> 7,
	);
	
$dzien_tyg_en=array(
		"Monday"		=> 1,
		"Tuesday"		=> 2,
		"Wednesday"		=> 3,
		"Thursday"		=> 4,
		"Friday"		=> 5,
		"Saturday"		=> 6,
		"Sunday"		=> 7,
	);

$tmp['date'] = date("d-m-Y"); //'09-07-2009';
$tmp['day_of_week']['en'] = date('l', strtotime($tmp['date']));
$tmp['day']=$dzien_tyg_en[$tmp['day_of_week']['en']];
$tmp['godz']=date("H");
$tmp['min']=date("i");
$tmp['czas']=$tmp['godz']+($tmp['min']/100);
$tmp['date'] = date("Y-m-d"); //'2009-07-09';

$i=0;
$zapytanie = "SELECT * from szkola.plan_lekcji where nauczyciel_id=".$_SESSION['id'].";";
$wynik = mysql_query($zapytanie);
while ($row = mysql_fetch_array($wynik)) 
{
	if($i==0)
	{
		$lesson['id']=$row['id'];
		$balabala=1;
		$lesson['godzina_lekcyjna']=$row['godzina_lekcyjna'];
		$lesson['klasa']=$row['klasa_ID'];
		$lesson['przedmiot']=$row['przedmiot_ID'];
		$lesson['dzien_tygodnia']=$row['dzien_tygodnia'];
		
		$day['dzien']=$dzien_tyg[$row['dzien_tygodnia']];
		if ($tmp['day'] > $day['dzien'])
		{
			$day['min_dzien']=7-$tmp['day']+$day['dzien'];
		}
		else
		{
			$day['min_dzien']=$day['dzien']-$tmp['day'];
		}
		$time['id']=$row['godzina_lekcyjna']-1;
		$time['godz_S']=$godz_l_S[$time['id']];
		$time['min_S']=$min_l_S[$time['id']];
		$time['czas']=$time['godz_S']+($time['min_S']/100);
		if($day['min_dzien'] == 0) 
		{
			if (($tmp['czas'] > $time['czas']) && ($tmp['czas'] < $czas_E[$time['id']]))
			{
				$active=true;
				break;
			}
			else if ($tmp['czas'] > $time['czas'])
			{
				$day['min_dzien']=7;
				$time['min_czas']=$time['czas'];
			}
			else
			{
				$time['min_czas']=$time['czas']-$tmp['czas'];
			}
		}
		else
		{
			$time['min_czas']=$time['czas'];
		}
	}
	else
	{
		$day2['dzien']=$dzien_tyg[$row['dzien_tygodnia']];
		if ($tmp['day'] > $day2['dzien'])
		{
			$day2['min_dzien']=7-$tmp['day']+$day2['dzien'];
		}
		else
		{
			$day2['min_dzien']=$day2['dzien']-$tmp['day'];
		}
		$time2['id']=$row['godzina_lekcyjna']-1;
		$time2['godz_S']=$godz_l_S[$time2['id']];
		$time2['min_S']=$min_l_S[$time2['id']];
		$time2['czas']=$time2['godz_S']+($time2['min_S']/100);
		if($day2['min_dzien'] == 0) 
		{
			if (($tmp['czas'] > $time2['czas']) && ($tmp['czas'] < $czas_E[$time2['id']]))
			{
				$active=true;
				
				$lesson['id']=$row['id'];
				$lesson['godzina_lekcyjna']=$row['godzina_lekcyjna'];
				$lesson['klasa']=$row['klasa_ID'];
				$lesson['przedmiot']=$row['przedmiot_ID'];
				$lesson['dzien_tygodnia']=$row['dzien_tygodnia'];
				
				$time['id']=$row['godzina_lekcyjna']-1;
				$time['godz_S']=$godz_l_S[$time['id']];
				$time['min_S']=$min_l_S[$time['id']];
				$time['czas']=$time['godz_S']+($time['min_S']/100);
		
				break;
			}
			else if ($tmp['czas'] > $time2['czas'])
			{
				$day2['min_dzien']=7;
				$time2['min_czas']=$time2['czas'];
			}
			else
			{
				$time2['min_czas']=$time2['czas']-$tmp['czas'];
			}
		}
		else
		{
			$time2['min_czas']=$time2['czas'];
		}
		
		if ($day2['min_dzien']<$day['min_dzien'])
		{
			$lesson['id']=$row['id'];
			$lesson['godzina_lekcyjna']=$row['godzina_lekcyjna'];
			$lesson['klasa']=$row['klasa_ID'];
			$lesson['przedmiot']=$row['przedmiot_ID'];
			$lesson['dzien_tygodnia']=$row['dzien_tygodnia'];
			
			$time['id']=$row['godzina_lekcyjna']-1;
			$time['godz_S']=$godz_l_S[$time['id']];
			$time['min_S']=$min_l_S[$time['id']];
			$time['czas']=$time['godz_S']+($time['min_S']/100);
				
			$day['min_dzien']=$day2['min_dzien'];
			$time['min_czas']=$time2['min_czas'];
		}
		else if ($day2['min_dzien']==$day['min_dzien'])
		{
			if($time2['min_czas']<$time['min_czas'])
			{
				$lesson['id']=$row['id'];
				$lesson['godzina_lekcyjna']=$row['godzina_lekcyjna'];
				$lesson['klasa']=$row['klasa_ID'];
				$lesson['przedmiot']=$row['przedmiot_ID'];
				$lesson['dzien_tygodnia']=$row['dzien_tygodnia'];
			
				$time['id']=$row['godzina_lekcyjna']-1;
				$time['godz_S']=$godz_l_S[$time['id']];
				$time['min_S']=$min_l_S[$time['id']];
				$time['czas']=$time['godz_S']+($time['min_S']/100);
				
				$day['min_dzien']=$day2['min_dzien'];
				$time['min_czas']=$time2['min_czas'];
			}
		}
	}
	$i++;
}

echo "<div style = \"margin-top:65px; font: 18px Verdana, sans-serif;\">";

//sprawdzenie czy lekcja już została utworzona
if(isset($active))
{
	$zapytanie = "SELECT * from szkola.temat_lekcji where klasa_id=".$lesson['klasa'].";";
	$wynik = mysql_query($zapytanie);
	while($w1 = mysql_fetch_array($wynik))
	{
		if(($w1['data'] == $tmp['date']) && ($w1['godzina_lekcyjna'] == $lesson['godzina_lekcyjna']))
		{
			$active=false;
			$lesson['lekcja']=$w1['lekcja_ID'];
			break;
		}
	}
}

//WYŚWIETLENIE INFORMACJI W GŁÓWNYM OKNIE
if (isset($active) && $active)
{
	$zapytanie = "SELECT * from szkola.klasa where klasa_id=".$lesson['klasa'].";";
	$wynik = mysql_query($zapytanie);
	$w1 = mysql_fetch_row($wynik);
	
	$zapytanie = "SELECT nazwa_przedmiotu from szkola.przedmiot where przedmiot_id=".$lesson['przedmiot'].";";
	$wynik = mysql_query($zapytanie);
	$w2 = mysql_fetch_row($wynik);
	
	echo "
	<form method=\"POST\" action=\"funkcje.php?akcja=dziennik\">
		<input type=\"hidden\" name=\"klasa\" value=\"".$lesson['klasa']."\" />	
		<input type=\"hidden\" name=\"przedmiot\" value=\"".$lesson['przedmiot']."\" />
		<input type=\"hidden\" name=\"godzina_lekcyjna\" value=\"".$lesson['godzina_lekcyjna']."\" />
		
		<button style=\"text-align:left; font: 18px Verdana, sans-serif; padding:10\" name=\"panel_edycji\" value=\"nowa_obecnosc\" type=\"submit\">
			Aktualnie masz zajęcia z klasą: ".$w1[1].$w1[2].", przedmiot: ".$w2[0]."<br>
			<b>Wejdź do kreatora nowej lekcji!</b>
		</button>
	</form>";
}
else if (isset($active) && !$active)
{
	$zapytanie = "SELECT * from szkola.klasa where klasa_id=".$lesson['klasa'].";";
	$wynik = mysql_query($zapytanie);
	$w1 = mysql_fetch_row($wynik);
	
	$zapytanie = "SELECT nazwa_przedmiotu from szkola.przedmiot where przedmiot_id=".$lesson['przedmiot'].";";
	$wynik = mysql_query($zapytanie);
	$w2 = mysql_fetch_row($wynik);
	
	echo "
	<form method=\"POST\" action=\"funkcje.php?akcja=dziennik\">
		<input type=\"hidden\" name=\"klasa\" value=\"".$lesson['klasa']."\" />	
		<input type=\"hidden\" name=\"przedmiot\" value=\"".$lesson['przedmiot']."\" />
		<input type=\"hidden\" name=\"lekcja\" value=\"".$lesson['lekcja']."\" />
		
		<button style=\"background-color:#99FF99; text-align:left; font: 18px Verdana, sans-serif; padding:10\" type=\"submit\">
			Aktualnie masz zajęcia z klasą: ".$w1[1].$w1[2].", przedmiot: ".$w2[0]."<br>
			<b>Wejdź do dziennika danej lekcji!</b>
		</button>
	</form>";
}
else if (isset($lesson))
{
	if ($time['min_S']==0)
	{
		$text="00";
	}
	else
	{
		$text=$time['min_S'];
	}
	echo "
	<button style=\"background-color:#DDDDDD; border:1px solid black; text-align:left; font: 18px Verdana, sans-serif; padding:10\" type=\"submit\">
		Następna lekcja odbędzie się dnia: <b>".$lesson['dzien_tygodnia']."</b><br>O godz.: <b>".$time['godz_S'].":".$text."</b>
	</button>";
}

echo "
	</div>";
}

function dziennik_lekcje()
{ 
$godz_l_S=array(8,8,9,10,11,12,13,14);
$min_l_S=array(0,55,50,45,40,35,30,25);

$dzien_tyg_en=array(
		"Monday"		=> "Poniedziałek",
		"Tuesday"		=> "Wtorek",
		"Wednesday"		=> "Środa",
		"Thursday"		=> "Czwartek",
		"Friday"		=> "Piątek",
		"Saturday"		=> "Sobota",
		"Sunday"		=> "Niedziela",
	);

//data
$tmp['date'] = date("d-m-Y"); //'09-07-2009';
$tmp['day_of_week']['en'] = date('l', strtotime($tmp['date']));
$tmp['day_of_week']['pl'] = $dzien_tyg_en[$tmp['day_of_week']['en']];
$tmp['date2'] = date("Y-m-d"); //'09-07-2009';

//oszacowanie ID następnej lekcji
$zapytanie = "SELECT lekcja_id from szkola.temat_lekcji order by lekcja_id desc;";
$wynik = mysql_query($zapytanie);	
$row = mysql_fetch_row($wynik);
$lekcja = $row[0]+1;

if ($min_l_S[$_POST['godzina_lekcyjna']-1]==0)
{
	$text="00";
}
else
{
	$text=$min_l_S[$_POST['godzina_lekcyjna']-1];
}

// NAGŁÓWKI
echo '<div>';

//	FORMULARZ DODAWANIA NOWEJ LEKCJI
echo"
	<form method=\"POST\" action=\"funkcje.php?akcja=dziennik\">
		<input type=\"hidden\" name=\"klasa\" value=\"".$_POST['klasa']."\" />	
		<input type=\"hidden\" name=\"przedmiot\" value=\"".$_POST['przedmiot']."\" />
		<input type=\"hidden\" name=\"lekcja\" value=\"".$lekcja."\" />
		<input type=\"hidden\" name=\"data\" value=\"".$tmp['date2']."\" />
		<input type=\"hidden\" name=\"godzina_lekcyjna\" value=\"".$_POST['godzina_lekcyjna']."\" />
		
		Data: ".$tmp['date']."
		</br>
		Dzień tygodnia: ".$tmp['day_of_week']['pl']."
		</br>
		Godzina: ".$godz_l_S[$_POST['godzina_lekcyjna']-1].":".$text."
		</br></br>
		<b>Temat lekcji:</b> <input style=\"width:40%\" type=\"text\" name=\"obecnosc_nazwa\">";
		
//tutaj koniec
if (isset($_POST['dziennik_obecnosc']) && $_POST['dziennik_obecnosc']=="blad")
{
	echo "
		<a style=\"color:red\"> Aby zapisać zmiany niezbędne jest uzupełnienie pola! </a>";
}

echo "
</br>
</br>
<table style = \"margin-top:0px; margin-bottom:0px; border:0;\">
<tr style=\"height:60px; border:0;\">
	<td colspan=2; style=\"width:30%; border-bottom: 1px solid black; border-top: 3px solid black; text-align:center; vertical-align: middle; margin: auto; padding: 0; border: 3px solid black;\"><b>Lista uczniów</b></td>		
	<td style=\"width:10%; border-bottom: 3px solid black; border-top: 3px solid black; border-right: 3px solid black; text-align:center; vertical-align: middle; margin: auto; padding: 0\"><b>Obecność</b></td>
	<td style=\"width:60%; text-align:center; vertical-align: middle; margin: auto; padding: 0; border:0\"><b></b></td>
</tr>
";

//////////////////
//WYPISANIE DANYCH
$j=0;
$zapytanie = "SELECT * from szkola.system_obecnosci;";
$syst_ocen = mysql_query($zapytanie);	
while ($row = mysql_fetch_row($syst_ocen)) 
{
	$arr1[$j]=$row[0];
	$arr2[$j]=$row[1];
	
	//$arr2[$j]=$row[1];
	$j++;
}

$k=0;
$zapytanie = "SELECT a.uczen_id, a.profil_id, s.nazwisko, s.imie from szkola.uczen a, szkola.profil s where a.klasa_id=".$_POST['klasa']." AND a.profil_id=s.profil_id order by s.nazwisko;";
$wynik = mysql_query($zapytanie);	
while ($row = mysql_fetch_row($wynik)) 
{
$k++;
$text = $row[2]." ".$row[3];
echo "
<tr style=\"border:0;\">
	<td style=\"width:3%; padding-bottom: 0; border-bottom: 1px dotted black; border-right: 3px solid black; border-left: 3px solid black;\">".$k.". </td>
	<td style=\"width:27%; border-bottom: 1px dotted black; border-right: 3px solid black; border-left: 3px solid black; padding-bottom: 0;\">".$text."</td>";
	
	// WYPISZ OBECNOŚCI
	
	//$zapytanie = "SELECT * from szkola.system_oceniania where typ_oceny_id=(SELECT typ_oceny_id from szkola.oceny where uczen_id=".$row[0]." AND rodzaj_oceny_id=".$arr3[$j].")";
	//$oceny = mysql_query($zapytanie);
	//$row1 = mysql_fetch_row($oceny);
			
	//x
	// EDYCJA OBECNOŚCI
	$t1 = "
		<select style=\"width:60%\" name=\"".$row[0]."\" size=\"1\">
		<option value=\"".$arr1[0]."\">".$arr2[0]."</option>";
			for ($i=0; $i<$j; $i++) 
			{
				if ($arr2[$i]==null)
				{
					$text="O";
				}
				else
				{
					$text=$arr2[$i];
				}
				
				$t1 = $t1." 
					   <option value=\"".$arr1[$i]."\">".$text."</option>";
			}
			$t1 = $t1."
			</select>";
				
			// info do zapisania obecności
			//$param3=$j;
			
		echo "
			<td style=\"width:10%; border-bottom: 1px dotted black; border-right: 3px solid black; text-align:center; vertical-align: middle; padding: 3 0 5 0;\">".$t1."</td>
			";
echo "
	<td style=\"border:0;\"></td>
</tr>";
}

echo "
<tr style=\"border:0; height:20px;\">
	<td colspan=3; style=\"border-top: 3px solid black; border-right: 0; border-left: 0; text-align:center; vertical-align: middle;\">
	<a >
		<button id=\"panel_edycji_act_save\" style=\"text-align:center; vertical-align:left; margin:15 0 0 0; display:inline;\" name=\"dziennik_obecnosc\" value=\"zapisz_obecnosc\" type=\"submit\"><b>ZAPISZ</b></button>
	</a>
	</td>
	<td style=\"border: 0;\"></td>
</tr>";

echo "
</table> ";

echo"
	</form>";
}

?>