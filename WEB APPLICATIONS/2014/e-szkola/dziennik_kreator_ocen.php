<?php

function dziennik_oceny()
{ 
// NAGŁÓWKI
echo '<div>';
echo "</br>";

//	FORMULARZ DODAWANIA NOWEJ OCENY
echo"
	<form method=\"POST\" action=\"funkcje.php?akcja=dziennik\">
		<input type=\"hidden\" name=\"klasa\" value=\"".$_POST['klasa']."\" />	
		<input type=\"hidden\" name=\"przedmiot\" value=\"".$_POST['przedmiot']."\" />
		<input type=\"hidden\" name=\"lekcja\" value=\"".$_POST['lekcja']."\" />
		
		<b>Nazwa oceny:</b> <input style=\"width:40%\" type=\"text\" name=\"ocena_nazwa\">";

if (isset($_POST['dziennik_oceny']) && $_POST['dziennik_oceny']=="blad")
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
	<td style=\"width:10%; border-bottom: 3px solid black; border-top: 3px solid black; border-right: 3px solid black; text-align:center; vertical-align: middle; margin: auto; padding: 0\"><b>Ocena</b></td>
	<td style=\"width:60%; text-align:center; vertical-align: middle; margin: auto; padding: 0; border:0\"><b></b></td>
</tr>
";

//////////////////
//WYPISANIE DANYCH
$j=0;
$zapytanie = "SELECT * from szkola.system_oceniania;";
$syst_ocen = mysql_query($zapytanie);	
while ($row = mysql_fetch_row($syst_ocen)) 
{
	$arr1[$j]=$row[0];
	$arr2[$j]=$row[1];
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
	
	// WYPISZ OCENY
	
	//$zapytanie = "SELECT * from szkola.system_oceniania where typ_oceny_id=(SELECT typ_oceny_id from szkola.oceny where uczen_id=".$row[0]." AND rodzaj_oceny_id=".$arr3[$j].")";
	//$oceny = mysql_query($zapytanie);
	//$row1 = mysql_fetch_row($oceny);
			
	//x
	// EDYCJA OCEN
	$t1 = "
		<select style=\"width:60%\" name=\"".$row[0]."\" size=\"1\">
		<option value=\"NULL\"> </option>";
			for ($i=0; $i<$j; $i++) 
			{
				$t1 = $t1." 
					   <option value=\"".$arr1[$i]."\">".$arr2[$i]."</option>";
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
		<button id=\"panel_edycji_extend\" style=\"width:120px; text-align:center; margin:15 30 0 0;\" type=\"submit\"><b>ANULUJ</b></button>
		<button id=\"panel_edycji_act_save\" style=\"text-align:center; vertical-align:left; margin:15 0 0 30; display:inline;\" name=\"dziennik_oceny\" value=\"zapisz_ocene\" type=\"submit\"><b>ZAPISZ</b></button>
	</a>
	</td>
	<td style=\"border: 0;\"></td>
</tr>";

echo "
</table> ";

echo"
	</form>";
}

function dziennik_edycja_oceny()
{ 
// NAGŁÓWKI
echo '<div>';
echo "</br>";

if (isset($_GET['edycja_ocena2']))
{
	$_POST['klasa']=$_GET['klasa'];
	$_POST['przedmiot']=$_GET['przedmiot'];
	$_POST['lekcja']=$_GET['lekcja'];
	$_POST['edycja_ocena2']=$_GET['edycja_ocena2'];
	$_POST['ocena']=$_GET['edycja_ocena2'];
}

$zapytanie = "SELECT nazwa from szkola.rodzaj_oceny where rodzaj_oceny_id=".$_POST['edycja_ocena2'].";";
$name_ocen = mysql_query($zapytanie);	
$row = mysql_fetch_row($name_ocen);

//	FORMULARZ DODAWANIA NOWEJ OCENY
echo"
	<form method=\"POST\" action=\"funkcje.php?akcja=dziennik\">
		<input type=\"hidden\" name=\"klasa\" value=\"".$_POST['klasa']."\" />	
		<input type=\"hidden\" name=\"przedmiot\" value=\"".$_POST['przedmiot']."\" />
		<input type=\"hidden\" name=\"lekcja\" value=\"".$_POST['lekcja']."\" />
		<input type=\"hidden\" name=\"ocena\" value=\"".$_POST['ocena']."\" />
		
		<b>Nazwa oceny:</b> <input style=\"width:40%\" type=\"text\" name=\"ocena_nazwa\" value=\"".$row[0]."\">";

if (isset($_POST['edycja_ocena2']) && $_POST['edycja_ocena2']=="blad")
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
	<td style=\"width:10%; border-bottom: 3px solid black; border-top: 3px solid black; border-right: 3px solid black; text-align:center; vertical-align: middle; margin: auto; padding: 0\"><b>Ocena</b></td>
	<td style=\"width:60%; text-align:center; vertical-align: middle; margin: auto; padding: 0; border:0\"><b></b></td>
</tr>
";

//////////////////
//WYPISANIE DANYCH
$j=0;
$zapytanie = "SELECT * from szkola.system_oceniania;";
$syst_ocen = mysql_query($zapytanie);	
while ($row = mysql_fetch_row($syst_ocen)) 
{
	$arr1[$j]=$row[0];
	$arr2[$j]=$row[1];
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
	
	// WYPISZ OCENY
	
	$zapytanie = "SELECT * from szkola.system_oceniania where typ_oceny_id=(SELECT typ_oceny_id from szkola.oceny where uczen_id=".$row[0]." AND rodzaj_oceny_id=".$_POST['ocena'].")";
	$oceny = mysql_query($zapytanie);
	$row1 = mysql_fetch_row($oceny);
			
	//x
	// EDYCJA OCEN
	$t1 = "
		<select style=\"width:60%\" name=\"".$row[0]."\" size=\"1\">
		<option value=\"".$row1[0]."\">".$row1[1]."</option>";
			for ($i=0; $i<$j; $i++) 
			{
				$t1 = $t1." 
					   <option value=\"".$arr1[$i]."\">".$arr2[$i]."</option>";
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
		<button id=\"panel_edycji_extend\" style=\"width:120px; text-align:center; margin:15 30 0 0;\" type=\"submit\"><b>ANULUJ</b></button>
		<button id=\"panel_edycji_act_save\" style=\"text-align:center; vertical-align:left; margin:15 0 0 10; display:inline;\" name=\"edycja_ocena2\" value=\"usun\" type=\"submit\"><b>USUŃ</b></button>
		<button id=\"panel_edycji_act_save\" style=\"text-align:center; vertical-align:left; margin:15 0 0 10; display:inline;\" name=\"edycja_ocena2\" value=\"zapisz\" type=\"submit\"><b>ZAPISZ</b></button>
	</a>
	</td>
	<td style=\"border: 0;\"></td>
</tr>";

echo "
</table> <br>";

echo"
	</form>";
}

?>