<?php
include 'dziennik_kreator_ocen.php';
//include 'dziennik_kreator_lekcji.php';

function dziennik()
{
if (isset($_POST['obecnosc_zapisz']) || isset($_POST['ocena_zapisz']) || (isset($_POST['dziennik_oceny']) && $_POST['dziennik_oceny'] == "zapisz_ocene") 
    || (isset($_POST['dziennik_obecnosc']) && $_POST['dziennik_obecnosc'] == "zapisz_obecnosc")
    || (isset($_POST['edycja_ocena2']) && ($_POST['edycja_ocena2'] == "zapisz" || $_POST['edycja_ocena2'] == "usun")))
{
    zapisz_dziennik();
}

/*
if (isset($_GET['edycja_ocena']))
{
    $_POST['klasa']=$_GET['klasa'];
    $_POST['przedmiot']=$_GET['przedmiot'];
    $_POST['lekcja']=$_GET['lekcja'];
}*/

if (isset($_GET['edycja_ocena2']) || (isset($_POST['edycja_ocena2']) && $_POST['edycja_ocena2']=="blad"))
{
    $title="Dziennik / Edycja Oceny";
    $disabled=false;
}
else if (isset($_POST['panel_edycji']) && ($_POST['panel_edycji']=="nowa_ocena"))
{
    $title="Dziennik / Nowa Ocena";
    $disabled=false;
}
else if (isset($_POST['panel_edycji']) && ($_POST['panel_edycji']=="nowa_obecnosc"))
{
    $title="Dziennik / Nowa Lekcja";
    $disabled=true;
}
else
{
    $title="Dziennik";
    $disabled=false;
}

echo "<div style = \"margin-top:65px; font: 18px Verdana, sans-serif;\">
<b>".$title."</b>
</div>";

if (isset($_GET['edycja_ocena2']) || (isset($_POST['edycja_ocena2']) && $_POST['edycja_ocena2']=="blad"))
{
    dziennik_edycja_oceny();
}

// dla tworzenia dziennika ograniczenia
if (isset($disabled) && $disabled)
{
    $text_dis="disabled=\"disabled\"";
}
else
{
    $text_dis="";
}

//
$i=0;

// KLASA
if (isset($_POST['klasa']))
{
    $zapytanie = "SELECT * from szkola.klasa where klasa_id=".$_POST['klasa'].";";
    $wynik = mysql_query($zapytanie);	
    $row = mysql_fetch_row($wynik);
    
    $text=$row[1].$row[2];
    $text2=$_POST['klasa'];
}
else
{
    $text="";
    $text2="";
}

echo "<div style = \"margin-top:15px; margin-bottom:0px;\">";	
echo"
<form method=\"POST\" action=\"funkcje.php?akcja=dziennik\">	
Wybierz Klasę:
<select name=\"klasa\" size=\"1\" ".$text_dis." onChange=\"this.form.submit()\">
<option value=\"".$text2."\">".$text."</option>";
	$zapytanie = "SELECT klasa_id from szkola.plan_lekcji where nauczyciel_id=".$_SESSION['id'].";";
	$wynik = mysql_query($zapytanie);	
	while ($row = mysql_fetch_row($wynik)) 
	{
        $s = FALSE;
        for ($j = 0; $j < sizeof($arr); $j++) {
            if ($row[0] == $arr[$j])
            {
                $s = TRUE;
                break;
            }
        }
        
        if ($s)
        {
            break;
        }
        else
        {
            $arr[sizeof($arr)] = $row[0];
        }
        
        $i++;
        $zapytanie = "SELECT poziom, podklasa from szkola.klasa where klasa_id=".$row[0].";";
        $wynik2 = mysql_query($zapytanie);
        $klasa = mysql_fetch_row($wynik2);
        
        echo " <option value=\"".$row[0]."\">".$i.".  ".$klasa[0].$klasa[1]."</option>";
	}
	echo"
</select>
</form> ";

if (isset($_POST['klasa']))
{
    $i=0;
    unset($arr);
    
    // PRZEDMIOT
    if (isset($_POST['przedmiot']))
    {
        $zapytanie = "SELECT nazwa_przedmiotu from szkola.przedmiot where przedmiot_id=".$_POST['przedmiot'].";";
        $wynik = mysql_query($zapytanie);	
        $row = mysql_fetch_row($wynik);
    
        $text=$row[0];
        $text2=$_POST['przedmiot'];
    }
    else
    {
        $text="";
        $text2="";
    }
	
    echo"
    <form method=\"POST\" action=\"funkcje.php?akcja=dziennik\">
    <input type=\"hidden\" name=\"klasa\" value=\"".$_POST['klasa']."\" />	
    Wybierz Przedmiot:
    <select name=\"przedmiot\" size=\"1\" ".$text_dis." onChange=\"this.form.submit()\">
    <option value=\"".$text2."\">".$text."</option>";
        $zapytanie = "SELECT przedmiot_id from szkola.plan_lekcji where nauczyciel_id=".$_SESSION['id']." AND klasa_id=".$_POST['klasa'].";";
        $wynik = mysql_query($zapytanie);	
        while ($row = mysql_fetch_row($wynik)) 
        {
            $s = FALSE;
            for ($j = 0; $j < sizeof($arr); $j++) {
                if ($row[0] == $arr[$j])
                {
                    $s = TRUE;
                    break;
                }
            }
        
            if ($s)
            {
                break;
            }
            else
            {
                $arr[sizeof($arr)] = $row[0];
            }
            
            $i++;
            $zapytanie = "SELECT nazwa_przedmiotu from szkola.przedmiot where przedmiot_id=".$row[0].";";
            $wynik2 = mysql_query($zapytanie);
            $przed = mysql_fetch_row($wynik2);
        
            echo " <option value=\"".$row[0]."\">".$i.".  ".$przed[0]."</option>";
        }
        echo"
    </select>
    </form> ";
}

if (isset($_POST['przedmiot']) && !$disabled)
{
    $j=0;
    
    // LEKCJA
    if (isset($_POST['lekcja']))
    {
        $zapytanie = "SELECT temat_lekcji, data from szkola.temat_lekcji where lekcja_id=".$_POST['lekcja'].";";
        $wynik = mysql_query($zapytanie);	
        $row = mysql_fetch_row($wynik);
        
        $zapytanie = "SELECT lekcja_id from szkola.temat_lekcji where przedmiot_id = ".$_POST['przedmiot']." AND klasa_id = ".$_POST['klasa']." AND nauczyciel_id = ".$_SESSION['id']." ORDER BY data;";
        $wynik2 = mysql_query($zapytanie);	
        while ($row2 = mysql_fetch_row($wynik2)) 
        {
            $j++;
            
            if ($row2[0] == $_POST['lekcja']) break;
        }
        
        $text="Lekcja ".$j.". -  ".$row[0]." (".$row[1].")";
        $data=$row[1];
        $text2=$_POST['lekcja'];
    }
    else
    {
        $text="";
        $text2="";
    }
    
    $i=0;
    echo"
    <form method=\"POST\" action=\"funkcje.php?akcja=dziennik\">
    <input type=\"hidden\" name=\"klasa\" value=\"".$_POST['klasa']."\" />	
    <input type=\"hidden\" name=\"przedmiot\" value=\"".$_POST['przedmiot']."\" />
    Wybierz Lekcję:
    <select name=\"lekcja\" size=\"1\" onChange=\"this.form.submit()\">
    <option value=\"".$text2."\">".$text."</option>";
        $zapytanie = "SELECT * from szkola.temat_lekcji where przedmiot_id = ".$_POST['przedmiot']." AND klasa_id = ".$_POST['klasa']." AND nauczyciel_id = ".$_SESSION['id']." ORDER BY data;";
        $wynik = mysql_query($zapytanie);	
        while ($row = mysql_fetch_row($wynik)) 
        {
            $i++;
            
            if ($row[0]!=$_POST['lekcja'])
            {
                echo " <option value=\"".$row[0]."\">Lekcja ".$i.". -  ".$row[4]." (".$row[5].")</option>";
            }
        }
        echo"
    </select>
    </form> ";
    
    if ($i == 0 && !isset($_POST['lekcja']))
    {
        echo "<br>";
        echo "<a style=\"color:red\"> Nie przeprowadziłeś jeszcze żadnej lekcji</a>"; 
    }
}

if ($i == 0 && !isset($_POST['klasa']))
{
   echo "<br>";
   echo "Nie masz żadnej klasy"; 
}	

echo '</div>';	

if (isset($_POST['panel_edycji']) && ($_POST['panel_edycji']=="nowa_ocena"))
{
    dziennik_oceny();
}
else if (isset($_POST['panel_edycji']) && ($_POST['panel_edycji']=="nowa_obecnosc"))
{
    dziennik_lekcje();
}
else
{
    if (isset($_POST['lekcja']))
    {
        wyswietl_dziennik($j,$data); 
    }
}
}

/////////////////
/////////////////
//////////
// FUNKCJA 2
function wyswietl_dziennik($param1,$param2)
{
$oceny_height = 50;
$il_ocen = 11;
$ocena_kratka = $oceny_height /$il_ocen;

$obecnosc_height = 13.5;
$il_obecnosc = 3;
$obecnosc_kratka = $obecnosc_height /$il_obecnosc;


//pobranie dat zaczynając od bierzącej lekcji
$spr=false;
$i=0;
$j=0;
$zapytanie = "SELECT data, lekcja_id from szkola.temat_lekcji where klasa_id=".$_POST['klasa']." AND przedmiot_id=".$_POST['przedmiot']." AND nauczyciel_ID=".$_SESSION['id']." order by data DESC;";
$daty_lekcji = mysql_query($zapytanie);
while ($row = mysql_fetch_row($daty_lekcji)) 
{
    if ($row[0]==$param2 || $spr)
    {
        $arr[$i]=$row[0];
        $arr2[$i]=$row[1];
        
        // rodzaje ocen od danego dnia
        $zapytanie = "SELECT rodzaj_oceny_id, nazwa from szkola.rodzaj_oceny where lekcja_id=".$arr2[$i]." order by rodzaj_oceny_id DESC;";
        $oceny = mysql_query($zapytanie);
        while ($row1 = mysql_fetch_row($oceny)) 
        {
            //rodzaj oceny
            $arr3[$j]=$row1[0];
            //z lekcji
            $arr4[$j]=$row[1];
            $arr5[$j]=$row1[1];
            $arr6[$j]=$row[0];
            $j++;
        }
        
        $i++;
        
        if (!$spr) $spr=true;
    }
}

if ($il_ocen>sizeof($arr)+1 && isset($arr3))
{
    $j=sizeof($arr3)-1;
}
else
{
    $j=$il_ocen-1;
}
$border1="";
$border2="";

// NAGŁÓWKI
echo '<div>';
echo "</br>";
echo "
<table style = \"margin-top:0px; border:0;\">
<tr style=\"border-bottom: 1px solid black; border-top: 3px solid black; height:50px;\">
    <td colspan=2; rowspan=3; style=\"width:27%; text-align:center; vertical-align: middle; margin: auto; padding: 0; border: 3px solid black;\"><b>Lista uczniów</b></td>	
    <td colspan=".$il_obecnosc."; style=\"width:".$obecnosc_height."%; text-align:center; vertical-align: middle; margin: auto; padding: 0\"><b>Obecność</b></td>	
    <td colspan=".$il_ocen."; style=\"width:".$oceny_height."%; text-align:center; vertical-align: middle; margin: auto; padding: 0\"><b>Oceny</b></td>
</tr>
<tr style=\"border: 1px solid black; height:10px;\">
    <td colspan=".$il_obecnosc."; style=\"width:10%; text-align:center; vertical-align: middle; margin: auto; padding: 0; font: 10px Verdana, sans-serif;\">lekcja nr</td>";
    //<td rowspan=2; colspan=".$il_ocen."; style=\"width:".$oceny_height."%; text-align:center; vertical-align: middle; margin: auto; padding: 0\"></td>
    // NAGŁÓWKI OCENY
    for ($i=0; $i<$il_ocen; $i++)
    {
        if($i>0) $border1 = "border-left: 1px dotted black; ";
        if($i!=$il_ocen-1) $border2 = "border-right: 1px dotted black; ";
        else $border2 = "";
        
        if (isset($arr3) && $il_ocen - $i <= sizeof($arr3))
        {
            if ($arr4[$j]==$_POST['lekcja'])
            {
                echo "
                <td rowspan=2; style=\"width:".$ocena_kratka."%; ".$border1.$border2." font: 14px Verdana, sans-serif; text-align:center; vertical-align: middle; padding: 3 0 5 0; background-color:66CC66;\">
                    <div id=\"sMenu\">
                        <ul>
                            <li>
                                <a style=\"width:10px\" href=\"#\">".($j+1)."</a>
                                <ul>
                                    <li><a href=\"funkcje.php?akcja=dziennik&edycja_ocena2=".$arr3[$j]."&klasa=".$_POST['klasa']."&przedmiot=".$_POST['przedmiot']."&lekcja=".$_POST['lekcja']."\">".$arr5[$j]."<br>(".$arr6[$j].")</a></li>
                                </ul>
                            </li>
                        </ul>
                    </div>
                </td>
                ";
            }
            else
            {
                echo "
                <td rowspan=2; style=\"width:".$ocena_kratka."%; ".$border1.$border2." font: 14px Verdana, sans-serif; text-align:center; vertical-align: middle; padding: 3 0 5 0;\">
                    <div id=\"sMenu\">
                        <ul>
                            <li>
                                <a style=\"width:10px\" href=\"#\">".($j+1)."</a>
                                <ul>
                                    <li><a href=\"funkcje.php?akcja=dziennik&edycja_ocena2=".$arr3[$j]."&klasa=".$_POST['klasa']."&przedmiot=".$_POST['przedmiot']."&lekcja=".$_POST['lekcja']."\">".$arr5[$j]."<br>(".$arr6[$j].")</a></li>
                                </ul>
                            </li>
                        </ul>
                    </div>
                </td>
                ";
            }
            
            $j--;
        }
        else
        {
            echo "
            <td rowspan=2; style=\"width:".$ocena_kratka."%; ".$border1.$border2." font: 14px Verdana, sans-serif; text-align:center; vertical-align: middle; padding: 3 0 5 0; background-color:#BBBBBB;\">x</td>
            ";
        }
    }
echo "
</tr>
<tr style=\"border-bottom: 3px solid black; height:10px;\">";
    //<td colspan=".$il_obecnosc."; style=\"width:10%; text-align:center; vertical-align: middle; margin: auto; padding: 0\"></td>	
    // NAGŁÓWKI OBECNOŚCI
    for ($i=0; $i<$il_obecnosc; $i++)
    {
        if($i>0) $border1 = "border-left: 1px dotted black; ";
        if($i!=$il_obecnosc-1) $border2 = "border-right: 1px dotted black; ";
        else $border2 = "";
        
        $wart = $param1-$il_obecnosc+1+$i;
        
        if ($wart<1)
        {
            echo "
            <td style=\"width:".$obecnosc_kratka."%; ".$border1.$border2." font: 14px Verdana, sans-serif; text-align:center; vertical-align: middle; padding: 3 0 5 0; background-color:#BBBBBB;\">x</td>
            ";
        }
        else if ($wart == $param1)
        {
            echo "
            <td style=\"width:".$obecnosc_kratka."%; ".$border1.$border2." font: 14px Verdana, sans-serif; text-align:center; vertical-align: middle; padding: 3 0 5 0; background-color:66CC66;\">".$wart."</td>
            ";
        }
        else
        {
            echo "
            <td style=\"width:".$obecnosc_kratka."%; ".$border1.$border2." font: 14px Verdana, sans-serif; text-align:center; vertical-align: middle; padding: 3 0 5 0;\">".$wart."</td>
            ";
        }
    }
echo "
</tr>
";

//  EDYCJA OBECNOSCI (obsluga cz1)
    echo"
        <form method=\"POST\" action=\"funkcje.php?akcja=dziennik\">
            <input type=\"hidden\" name=\"klasa\" value=\"".$_POST['klasa']."\" />	
            <input type=\"hidden\" name=\"przedmiot\" value=\"".$_POST['przedmiot']."\" />
            <input type=\"hidden\" name=\"lekcja\" value=\"".$_POST['lekcja']."\" />";

//////////////////
//WYPISANIE DANYCH
$k=0;
$zapytanie = "SELECT a.uczen_id, a.profil_id, s.nazwisko, s.imie from szkola.uczen a, szkola.profil s where a.klasa_id=".$_POST['klasa']." AND a.profil_id=s.profil_id order by s.nazwisko;";
$wynik = mysql_query($zapytanie);	
while ($row = mysql_fetch_row($wynik)) 
{
$k++;
$text = $row[2]." ".$row[3];
echo "
<tr style=\"border-right: 3px solid black; border-left: 3px solid black;\">
    <td style=\"width:3%; padding-bottom: 0;\">".$k.". </td>
    <td style=\"width:27%; border-right: 3px solid black; padding-bottom: 0;\">".$text."</td>";
    //<td style=\"width:13.5%;\">...</td>";
    
    // WYPISZ OBECNOŚCI
    $j=$il_obecnosc-1;
    for ($i=0; $i<$il_obecnosc; $i++)
    {
        if($i>0) $border1 = "border-left: 1px dotted black; ";
        if($i!=$il_obecnosc-1) $border2 = "border-right: 1px dotted black; ";
        else $border2 = "";
        
        if ($i+$param1-$il_obecnosc>=0)
        {
            $zapytanie = "SELECT * from szkola.system_obecnosci where status_id=(SELECT status_id from obecnosci where uczen_id=".$row[0]." AND klasa_id=".$_POST['klasa']." AND przedmiot_id=".$_POST['przedmiot']." AND lekcja_id=".$arr2[$j].")";
            $obecnosc = mysql_query($zapytanie);
            $row1 = mysql_fetch_row($obecnosc);
            
            // EDYCJA OBECNOSCI (obsluga cz2)
            if (isset($_POST['edycja_obecnosc']) && $_POST['edycja_obecnosc'] == $i)
            {
                $t1 = "
                <select name=\"".$row[0]."\" size=\"1\">
                <option value=\"".$row1[0]."\">".$row1[1]."</option>";
                $zapytanie = "SELECT * from szkola.system_obecnosci;";
                $syst_obec = mysql_query($zapytanie);	
                while ($row2 = mysql_fetch_row($syst_obec)) 
                {
                    if ($row2[1]==null)
                    {
                        $t2="O";
                    }
                    else
                    {
                        $t2=$row2[1];
                    }
        
                    $t1 = $t1." 
                           <option value=\"".$row2[0]."\">".$t2."</option>";
                }
                $t1 = $t1."
                </select>";
                
                // info do zapisania obecności
                $param3=$j;
            }
            else
            {
                $t1 = $row1[1];
            }
            
            // Wyświetlanie obecności (rozróżnienie kolorów kolumn)
            if ($i+1==$il_obecnosc)
            {
                //gdy kolumna zgodna z wybraną lekcją
                echo "
                 <td style=\"width:".$obecnosc_kratka."%; ".$border1.$border2."; text-align:center; vertical-align: middle; padding: 3 0 5 0; background-color:99FF99;\">".$t1."</td>
                ";
            }
            else
            {
                //lekcje wcześniejsze
                echo "
                <td style=\"width:".$obecnosc_kratka."%; ".$border1.$border2."; text-align:center; vertical-align: middle; padding: 3 0 5 0\">".$t1."</td>
                ";
            }
        }
        else
        {
            //szara kolumna dla lekcji nie odbytych
            echo "
            <td style=\"width:".$obecnosc_kratka."%; ".$border1.$border2."; background-color:#DDDDDD;\">...</td>
            ";
        }
        
        $j--;
    }
    
    // WYPISZ OCENY
    if (isset($arr3) && $il_ocen>sizeof($arr)+1)
    {
        $j=sizeof($arr3)-1;
    }
    else
    {
        $j=$il_ocen-1;
    }
    
    for ($i=0; $i<$il_ocen; $i++)
    {
        if($i>0) $border1 = "border-left: 1px dotted black; ";
        if($i!=$il_ocen-1) $border2 = "border-right: 1px dotted black; ";
        else $border2 = "";
            
        //if ($j +$il_ocen <= sizeof($arr3))
        if (isset($arr3) && $il_ocen - $i <= sizeof($arr3))
        {
            $zapytanie = "SELECT * from szkola.system_oceniania where typ_oceny_id=(SELECT typ_oceny_id from szkola.oceny where uczen_id=".$row[0]." AND rodzaj_oceny_id=".$arr3[$j].")";
            $oceny = mysql_query($zapytanie);
            $row1 = mysql_fetch_row($oceny);
            
            //x
            // EDYCJA OCEN (obsluga cz2)
            if (isset($_POST['edycja_ocena']) && $_POST['edycja_ocena'] == $i)
            {
                $t1 = "
                <select name=\"".$row[0]."\" size=\"1\">
                <option value=\"".$row1[0]."\">".$row1[1]."</option>
                <option value=\"NULL\"> </option>";
                    $zapytanie = "SELECT * from szkola.system_oceniania;";
                    $syst_ocen = mysql_query($zapytanie);	
                    while ($row2 = mysql_fetch_row($syst_ocen)) 
                    {
                        $t1 = $t1." 
                               <option value=\"".$row2[0]."\">".$row2[1]."</option>";
                    }
                    $t1 = $t1."
                    </select>";
                
                // info do zapisania obecności
                $param3=$j;
            }
            else
            {   
                $t1 = $row1[1];
            }
            
            //if ($arr4[$j]==$_POST['lekcja'])
            if ($arr4[$j]==$_POST['lekcja'])
            {
                echo "
                <td style=\"width:".$ocena_kratka."%; ".$border1.$border2."; text-align:center; vertical-align: middle; padding: 3 0 5 0; background-color:99FF99;\">".$t1."</td>
                ";
            }
            else
            {
                echo "
                <td style=\"width:".$ocena_kratka."%; ".$border1.$border2."; text-align:center; vertical-align: middle; padding: 3 0 5 0; \">".$t1."</td>
                ";
            }
            
            $j--;
        }
        else
        {
            //szara kolumna dla lekcji nie odbytych (oceny z lekcji nieodbytych)
            echo "
            <td style=\"width:".$ocena_kratka."%; ".$border1.$border2."; background-color:#DDDDDD;\">...</td>
            ";
        }
    }
echo "
</tr>";
}

if (isset($_POST['edycja_obecnosc']) || isset($_POST['edycja_ocena']))
{
    $b_height=3;
}
else
{
    $b_height=0;
}
///////////////
// PANEL EDYCJI
echo "
<tr height=20px; style=\"border-left: 0; border-right: 3px solid black;  border-bottom: 0;\">
    <td style=\"width:3%; padding-bottom: 0; border-top: 3px solid black; border-left: 0px solid black; border-right: 0px solid black;\"></td>
    <td style=\"width:27%; border-top: 3px solid black; border-right: 3px solid black; padding-bottom: 0; border-left: 0px solid black;\"></td>";
    //<td style=\"width:13.5%;\">...</td>";
    
    // EDYCJA OBECNOŚCI
    $j=$il_obecnosc-1;
    for ($i=0; $i<$il_obecnosc; $i++)
    {
        if($i>0) $border1 = "border-left: 1px dotted black; ";
        if($i!=$il_obecnosc-1) $border2 = "border-right: 1px dotted black; ";
        else $border2 = "";
        
        if ($i+$param1-$il_obecnosc>=0)
        {
            if ($i+1==$il_obecnosc)
            {
                //gdy kolumna zgodna z wybraną lekcją
                if (isset($_POST['edycja_obecnosc'])) //w edycji przycisk anuluj
                {
                    echo "                        
                        <td style=\"width:".$obecnosc_kratka."%; ".$border1.$border2."; padding: 6 0 6 0; border-bottom: ".$b_height."px solid black; border-top: 5px double black; background-color:66CC66;\">
                            <a >
                                <button id=\"panel_edycji_act\" type=\"submit\">a n u l u j</button>
                            </a>
                        </td>
                    ";
                }
                else //tradycyjny przycisk "edycja"
                {
                    echo " 
                    <td style=\"width:".$obecnosc_kratka."%; ".$border1.$border2."; padding: 6 0 6 0; border-bottom: ".$b_height."px solid black; border-top: 5px double black; background-color:66CC66;\">
                        <a >
                            <button id=\"panel_edycji\" name=\"edycja_obecnosc\" value=\"".$i."\" type=\"submit\">e d y c j a</button>
                        </a>
                    </td>";
                }
            }
            else
            {
                //lekcje wcześniejsze
                echo "
                <td style=\"width:".$obecnosc_kratka."%; ".$border1.$border2."; text-align:center; vertical-align: middle; padding: 3 0 5 0; border-bottom: ".$b_height."px solid black; border-top: 5px double black;\">...</td>
                ";
            }
        }
        else
        {
            //szara kolumna dla lekcji nie odbytych
            echo "
            <td style=\"width:".$obecnosc_kratka."%; ".$border1.$border2."; border-bottom: ".$b_height."px solid black; border-top: 5px double black; background-color:#BBBBBB;\">...</td>
            ";
        }
        
        $j--;
    }
    
    // EDYCJA OCENY
    if (isset($arr3) && $il_ocen>sizeof($arr)+1)
    {
        $j=sizeof($arr3)-1;
    }
    else
    {
        $j=$il_ocen-1;
    }
    
    for ($i=0; $i<$il_ocen; $i++)
    {
        if($i>0) $border1 = "border-left: 1px dotted black; ";
        if($i!=$il_ocen-1) $border2 = "border-right: 1px dotted black; ";
        else $border2 = "";
        
        if (isset($arr3) && $il_ocen - $i <= sizeof($arr3))
        {
            if ($arr4[$j]==$_POST['lekcja'])
            {   
                //gdy kolumna zgodna z wybraną lekcją
                if (isset($_POST['edycja_ocena']) && $_POST['edycja_ocena']==$i) //w edycji przycisk anuluj
                {
                    echo "                        
                        <td style=\"width:".$ocena_kratka."%; ".$border1.$border2."; padding: 6 0 6 0; border-bottom: ".$b_height."px solid black; border-top: 5px double black; background-color:66CC66;\">
                            <a >
                                <button id=\"panel_edycji_act\" type=\"submit\">a n u l u j</button>
                            </a>
                        </td>
                    ";
                }
                /*
                else if (isset($_POST['edycja_ocena']))
                {
                    echo "
                    <td style=\"width:".$ocena_kratka."%; ".$border1.$border2."; padding: 6 0 6 0; border-bottom: 3px solid black; border-top: 5px double black; background-color:66CC66;\">...</td>";
                }*/
                else //tradycyjny przycisk "edycja"
                {
                    echo "
                    <td style=\"width:".$ocena_kratka."%; ".$border1.$border2."; padding: 6 0 6 0; border-bottom: ".$b_height."px solid black; border-top: 5px double black; background-color:66CC66;\">
                        <a >
                            <button id=\"panel_edycji\" name=\"edycja_ocena\" value=\"".$i."\" type=\"submit\">e d y c j a</button>
                        </a>
                    </td>";
                }
                
                /*
                echo "
                <td style=\"width:".$ocena_kratka."%; ".$border1.$border2."; text-align:center; vertical-align: middle; padding: 3 0 5 0; border-bottom: 3px solid black; border-top: 5px double black; background-color:66CC66;\">...</td>
                ";
                */
            }
            else
            {
                echo "
                <td style=\"width:".$ocena_kratka."%; ".$border1.$border2."; text-align:center; vertical-align: middle; padding: 3 0 5 0; border-bottom: ".$b_height."px solid black; border-top: 5px double black; \">...</td>
                ";
            }
            
            $j--;
        }
        else
        {
            //szara kolumna dla lekcji nie odbytych (oceny z lekcji nieodbytych)
            echo "
            <td style=\"width:".$ocena_kratka."%; ".$border1.$border2."; border-bottom: ".$b_height."px solid black; border-top: 5px double black; background-color:#BBBBBB;\">...</td>
            ";
        }
    }
echo "
</tr>";

//
//druga część panelu edycji
if (!isset($_POST['edycja_obecnosc']) && !isset($_POST['edycja_ocena']))
{
echo "
    <tr height=70px; style=\"border-left: 0; border-right: 3px solid black;  border-bottom: 0; border-top: 0;  border-top: 0;\">
        <td style=\"width:3%; padding-bottom: 0; border-left: 0px solid black; border-right: 0px solid black;\"></td>
        <td style=\"width:27%; border-right: 3px solid black; padding-bottom: 0; border-left: 0px solid black;\"></td>
        <td colspan=".$il_obecnosc."; style=\"width:".$obecnosc_height."%; border-top: 2px dotted black; border-bottom: 3px solid black; text-align:center; vertical-align: middle; margin: auto; padding: 0\">...</td>	
    
        <td colspan=".$il_ocen."; style=\"width:".$oceny_height."%; border-top: 2px dotted black; border-bottom: 3px solid black; text-align:center; vertical-align: middle; margin: auto; padding: 0\">
            <a >
                <button id=\"panel_edycji_extend\" name=\"panel_edycji\" value=\"nowa_ocena\" type=\"submit\">Dodaj ocenę</button>
            </a>
        </td>
        
    </tr>";
}

echo "
</table> ";

//  EDYCJA OBECNOSCI (obsluga cz3)
if (isset($_POST['edycja_obecnosc']))
{
    echo "
        <a >
            <button id=\"panel_edycji_act_save\" style=\"text-align:center;\" name=\"obecnosc_zapisz\" value=\"".$arr2[$param3]."\" type=\"submit\"><b>ZAPISZ</b></button>
        </a>";
}
//  EDYCJA OCEN (obsluga cz3)
if (isset($_POST['edycja_ocena']))
{
    echo "
        <a >
            <button id=\"panel_edycji_act_save\" style=\"text-align:center;\" name=\"ocena_zapisz\" value=\"".$arr3[$param3]."\" type=\"submit\"><b>ZAPISZ</b></button>
        </a>";
}

echo "
    </form> ";
    
echo '</div>';
}

/////////////////
/////////////////
//////////
// FUNKCJA 3
function zapisz_dziennik()
{
    if (isset($_POST['obecnosc_zapisz']))
    {
        $zapytanie = "SELECT uczen_id from szkola.uczen where klasa_id=".$_POST['klasa'].";";
        $obecnosc = mysql_query($zapytanie);
        while ($row = mysql_fetch_row($obecnosc)) 
        {
            $zapytanie = "SELECT status_id from szkola.obecnosci where klasa_id=".$_POST['klasa']." and przedmiot_id=".$_POST['przedmiot']." and lekcja_id=".$_POST['obecnosc_zapisz']." and uczen_id=".$row[0].";";
            $obecnosc1 = mysql_query($zapytanie);
            $row1 = mysql_fetch_row($obecnosc1);
            
            if ($_POST[$row[0]] != $row1[0])
            {
                $zapytanie = "UPDATE szkola.obecnosci SET status_id=".$_POST[$row[0]]." where klasa_id=".$_POST['klasa']." and przedmiot_id=".$_POST['przedmiot']." and lekcja_id=".$_POST['obecnosc_zapisz']." and uczen_id=".$row[0].";";
                $obecnosc2 = mysql_query($zapytanie);
            }
        }
    }
    else if (isset($_POST['ocena_zapisz']))
    {
        $i=0;
        $zapytanie = "SELECT uczen_id from szkola.uczen where klasa_id=".$_POST['klasa'].";";
        $ocena = mysql_query($zapytanie);
        while ($row = mysql_fetch_row($ocena)) 
        {
            $i++;
            $zapytanie = "SELECT typ_oceny_id from szkola.oceny where uczen_id=".$row[0]." and rodzaj_oceny_id=".$_POST['ocena_zapisz'].";";
            $ocena1 = mysql_query($zapytanie);
            $row1 = mysql_fetch_row($ocena1);
            
            if ($_POST[$row[0]] != $row1[0])
            {
                $zapytanie = "UPDATE szkola.oceny SET typ_oceny_id=".$_POST[$row[0]]." where uczen_id=".$row[0]." and rodzaj_oceny_id=".$_POST['ocena_zapisz'].";";
                $ocena2 = mysql_query($zapytanie);
            }
        }
    }
    else if (isset($_POST['dziennik_oceny']) && $_POST['dziennik_oceny'] == "zapisz_ocene")
    {
        if (isset($_POST['ocena_nazwa']) && $_POST['ocena_nazwa'] != "")
        {
            $zapytanie = "SELECT rodzaj_oceny_id from szkola.rodzaj_oceny order by rodzaj_oceny_id desc;";
            $rodz_oceny = mysql_query($zapytanie);
            $row = mysql_fetch_row($rodz_oceny);
            
            $id = $row[0];
            $id++;
            //$id=4;
            
            $zapytanie = "INSERT INTO szkola.rodzaj_oceny VALUES (".$id.",".$_POST['lekcja'].",".$_POST['przedmiot'].",".$_POST['klasa'].",'".$_POST['ocena_nazwa']."',null);";
            $nowy = mysql_query($zapytanie);
            
            if ($nowy)
            {
                $zapytanie = "SELECT ocena_id from szkola.oceny order by ocena_id desc;";
                $rodz_oceny = mysql_query($zapytanie);
                $row = mysql_fetch_row($rodz_oceny);
                
                $id2 = $row[0];
                $id2++;
                
                $zapytanie = "SELECT uczen_id from szkola.uczen where klasa_id=".$_POST['klasa'].";";
                $dodaj_ocene = mysql_query($zapytanie);
                while ($row = mysql_fetch_row($dodaj_ocene)) 
                {
                    $zapytanie = "INSERT INTO szkola.oceny VALUES (".$id2.",".$row[0].",".$_POST[$row[0]].",".$id.");";
                    $dodaj = mysql_query($zapytanie);
                    
                    $id2++;
                }
            }
            /*
            else
            {
                echo "<br/><br/><br/><br/><br/><br/><br/>".mysql_error(); 
            }*/
        }
        else
        {
            $_POST['panel_edycji']="nowa_ocena";
            $_POST['dziennik_oceny']="blad";
        }
    }
    else if (isset($_POST['dziennik_obecnosc']) && $_POST['dziennik_obecnosc'] == "zapisz_obecnosc")
    {
        if (isset($_POST['obecnosc_nazwa']) && $_POST['obecnosc_nazwa'] != "")
        {
            $zapytanie = "INSERT INTO szkola.temat_lekcji VALUES (".$_POST['lekcja'].",".$_POST['przedmiot'].",".$_SESSION['id'].",".$_POST['klasa'].",'".$_POST['obecnosc_nazwa']."','".$_POST['data']."',".$_POST['godzina_lekcyjna'].");";
            $nowy = mysql_query($zapytanie);
            
            if ($nowy)
            {
                $zapytanie = "SELECT uczen_id from szkola.uczen where klasa_id=".$_POST['klasa'].";";
                $obecnosc = mysql_query($zapytanie);
                while ($row = mysql_fetch_row($obecnosc)) 
                {
                    $zapytanie = "INSERT INTO szkola.obecnosci VALUES (".$row[0].",".$_POST['klasa'].",".$_POST['przedmiot'].",".$_POST['lekcja'].",".$_POST[$row[0]].");";
                    $obecnosc2 = mysql_query($zapytanie);
                }
            }
        }
        else
        {
            $_POST['panel_edycji']="nowa_obecnosc";
            $_POST['dziennik_obecnosc']="blad";
        }
    }
    else if (isset($_POST['edycja_ocena2']) && $_POST['edycja_ocena2']=="zapisz")
    {
        if (isset($_POST['ocena_nazwa']) && $_POST['ocena_nazwa'] != "")
        {
            $i=0;
            $zapytanie = "SELECT nazwa from szkola.rodzaj_oceny where rodzaj_oceny_id=".$_POST['ocena'].";";
            $ocena = mysql_query($zapytanie);
            $row = mysql_fetch_row($ocena);
            
            if ($_POST['ocena_nazwa'] != $row[0])
            {
                $zapytanie = "UPDATE szkola.rodzaj_oceny SET nazwa='".$_POST['ocena_nazwa']."' where rodzaj_oceny_ID=".$_POST['ocena'].";";
                $ocena2 = mysql_query($zapytanie);
            }
            
            $zapytanie = "SELECT uczen_id from szkola.uczen where klasa_id=".$_POST['klasa'].";";
            $ocena = mysql_query($zapytanie);
            while ($row = mysql_fetch_row($ocena)) 
            {
                $i++;
                $zapytanie = "SELECT typ_oceny_id from szkola.oceny where uczen_id=".$row[0]." and rodzaj_oceny_id=".$_POST['ocena'].";";
                $ocena1 = mysql_query($zapytanie);
                $row1 = mysql_fetch_row($ocena1);
            
                if ($_POST[$row[0]] != $row1[0])
                {
                    $zapytanie = "UPDATE szkola.oceny SET typ_oceny_id=".$_POST[$row[0]]." where uczen_id=".$row[0]." and rodzaj_oceny_id=".$_POST['ocena'].";";
                    $ocena2 = mysql_query($zapytanie);
                }
            }
        }
        else
        {
            $_POST['edycja_ocena2']="blad";
        }
    }
    else if (isset($_POST['edycja_ocena2']) && $_POST['edycja_ocena2']=="usun")
    {
        $i=0;
            
        $zapytanie = "SELECT uczen_id from szkola.uczen where klasa_id=".$_POST['klasa'].";";
        $ocena = mysql_query($zapytanie);
        while ($row = mysql_fetch_row($ocena)) 
        {
            $zapytanie = "DELETE FROM szkola.oceny WHERE uczen_id=".$row[0]." and rodzaj_oceny_id=".$_POST['ocena'].";";
            $ocena2 = mysql_query($zapytanie);
        }
        
        $zapytanie = "DELETE from szkola.rodzaj_oceny where rodzaj_oceny_ID=".$_POST['ocena'].";";
        $ocena2 = mysql_query($zapytanie);
    }
}
?>