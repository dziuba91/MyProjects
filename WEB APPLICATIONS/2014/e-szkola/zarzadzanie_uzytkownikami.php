<?php

function add_user()
{
	echo '<div id="container">';	
	echo"<form method=\"POST\" action=\"funkcje.php?akcja=nowy_user\">	
			Typ użytkonwika:
			<select name=\"prawa\" size=\"1\">
			<option value=\"1\">Nauczyciel</option>;
			<option value=\"2\">Dyrektor</option>;
			<option value=\"3\">Planista</option>;
			</select>
			<input type=\"submit\" value=\"Dalej\" name=\"Dalej\">
		</form> ";


	if (isset($_POST['Dalej']) && $_POST['prawa'] == 1)
	{
		echo"<form method=\"POST\" action=\"funkcje.php?akcja=nowy_user\">	
			Login:<input type=\"text\" name=\"naucz_login\" />
			Hasło:<input type=\"text\" name=\"naucz_haslo\" />
			Imie:<input type=\"text\" name=\"naucz_imie\" />
			Nazwisko:<input type=\"text\" name=\"naucz_nazwisko\" />
			Data urodzenia:<input type=\"text\" name=\"naucz_dat_ur\" />
			Pesel:<input type=\"text\" name=\"naucz_pesel\" />
			Przedmiot: <select name=\"przedmiot\" size=\"1\" >";
	
		$zapytanie = "SELECT nazwa_przedmiotu, przedmiot_ID from szkola.przedmiot;";
		$wynik = mysql_query($zapytanie);	
		while ($row = mysql_fetch_row($wynik)) 
		{
			echo " <option value=\"".$row[1]."\">".$row[0]."</option>";
		}
		
		echo "</select>
				<input type=\"submit\" value=\"Dodaj\" name=\"Dalej2\">
			</form> ";
	}
	
	if (isset($_POST['Dalej2']))
	{
		$login = $_POST['naucz_login'];
		$haslo = $_POST['naucz_haslo'];
		$imie = $_POST['naucz_imie'];
		$nazwisko = $_POST['naucz_nazwisko'];
		$dat_ur = $_POST['naucz_dat_ur'];
		$pesel = $_POST['naucz_pesel'];
		$przedmiot_ID = $_POST['przedmiot'];
		
		$zapytanie1 = "Select profil_ID from szkola.profil;";
		$wynik = mysql_query($zapytanie1);	
		while ($row = mysql_fetch_row($wynik)) 
		{
			$id=$row[0];
		}
		
		$id=$id+1;
		$zapytanie = "Insert into szkola.profil values ( ".$id.", '".$login."', '".$haslo."','nauczyciel','".$imie."','".$nazwisko."',".$pesel.", '".$dat_ur."')";
		$wynik = mysql_query($zapytanie);	
	
		$zapytanie2 = "SELECT nauczyciel_ID FROM szkola.nauczyciel ORDER BY nauczyciel_ID ASC;";
		$wynik = mysql_query($zapytanie2);	
		while ($row = mysql_fetch_row($wynik)) 
		{
			$id_naucz=$row[0];
		}
		
		$id_naucz=$id_naucz+1;
		
		$zapytanie = "Insert into szkola.nauczyciel values ( ".$id_naucz.", ".$przedmiot_ID.", ".$id.");";
		$wynik = mysql_query($zapytanie);
		if(!$wynik)
		{
			echo "Napotkano błąd podczas dodawania użytkownika. Spróbuj ponownie.";
		}
		else
		{
			echo "Dodawanie użytkownika zakończono z powodzeniem";
		}
	}
	
	if (isset($_POST['Dalej']) && $_POST['prawa'] == 2)
	{
		echo"<form method=\"POST\" action=\"funkcje.php?akcja=nowy_user\">	
			Login:<input type=\"text\" name=\"dyrek_login\" />
			Hasło:<input type=\"text\" name=\"dyrek_haslo\" />
			Imie:<input type=\"text\" name=\"dyrek_imie\" />
			Nazwisko:<input type=\"text\" name=\"dyrek_nazwisko\" />
			Data urodzenia:<input type=\"text\" name=\"dyrek_dat_ur\" />
			Pesel:<input type=\"text\" name=\"dyrek_pesel\" />
			<input type=\"submit\" value=\"Dodaj\" name=\"Dalej3\">
		</form> ";
	}
	
	if (isset($_POST['Dalej3']) )
	{
		$login = $_POST['dyrek_login'];
		$haslo = $_POST['dyrek_haslo'];
		$imie = $_POST['dyrek_imie'];
		$nazwisko = $_POST['dyrek_nazwisko'];
		$dat_ur = $_POST['dyrek_dat_ur'];
		$pesel = $_POST['dyrek_pesel'];
		
		$zapytanie1 = "Select profil_ID from szkola.profil;";
		$wynik = mysql_query($zapytanie1);	
		while ($row = mysql_fetch_row($wynik)) 
		{
			$id=$row[0];
		}
		$id=$id+1;
	
		$zapytanie = "Insert into szkola.profil values ( ".$id.", '".$login."', '".$haslo."','dyrektor','".$imie."','".$nazwisko."',".$pesel.", '".$dat_ur."')";
		$wynik = mysql_query($zapytanie);	
		if(!$wynik)
		{
			echo "Napotkano błąd podczas dodawania użytkownika. Spróbuj ponownie.";
		}
		else
		{
			echo "Dodawanie użytkownika zakończono z powodzeniem";
		}
	}
	
	if (isset($_POST['Dalej']) && $_POST['prawa'] == 3)
	{
		echo"<form method=\"POST\" action=\"funkcje.php?akcja=nowy_user\">	
			Login:<input type=\"text\" name=\"planista_login\" />
			Hasło:<input type=\"text\" name=\"planista_haslo\" />
			Imie:<input type=\"text\" name=\"planista_imie\" />
			Nazwisko:<input type=\"text\" name=\"planista_nazwisko\" />
			Data urodzenia:<input type=\"text\" name=\"planista_dat_ur\" />
			Pesel:<input type=\"text\" name=\"planista_pesel\" />
			<input type=\"submit\" value=\"Dodaj\" name=\"Dalej5\">
		</form> ";
	}
	
	if (isset($_POST['Dalej5']) )
	{
		$login = $_POST['planista_login'];
		$haslo = $_POST['planista_haslo'];
		$imie = $_POST['planista_imie'];
		$nazwisko = $_POST['planista_nazwisko'];
		$dat_ur = $_POST['planista_dat_ur'];
		$pesel = $_POST['planista_pesel'];
	
		$zapytanie1 = "Select profil_ID from szkola.profil;";
		$wynik = mysql_query($zapytanie1);	
		while ($row = mysql_fetch_row($wynik)) 
		{
			$id=$row[0];
		}
		$id=$id+1;
	
		$zapytanie = "Insert into szkola.profil values ( ".$id.", '".$login."', '".$haslo."','planista','".$imie."','".$nazwisko."',".$pesel.", '".$dat_ur."')";
		$wynik = mysql_query($zapytanie);	
		if(!$wynik)
		{
			echo "Napotkano błąd podczas dodawania użytkownika. Spróbuj ponownie.";
		}
		else
		{
			echo "Dodawanie użytkownika zakończono z powodzeniem";
		}
	}
	
	echo '</div>';
}

function delete_user()
{
	echo '<div id="container">';	
	echo"<form method=\"POST\" action=\"funkcje.php?akcja=usun_user\">	
		Typ użytkonwika:
		<select name=\"prawa2\" size=\"1\">
		<option value=\"1\">Nauczyciel</option>;
		<option value=\"2\">Dyrektor</option>;
		<option value=\"3\">Planista</option>;
		</select>
		<input type=\"submit\" value=\"Dalej\" name=\"Dalej4\">
	</form> ";

	if (isset($_POST['Dalej4']) && $_POST['prawa2'] == 1)
	{
		echo"<form method=\"POST\" action=\"funkcje.php?akcja=usun_user\">
			Imię i nazwisko: <select name=\"nauczyciel_usun\" size=\"1\" >";
		
		$zapytanie = "SELECT profil_ID, imie, nazwisko from szkola.profil where prawa = 'nauczyciel';";
		$wynik = mysql_query($zapytanie);	
		while ($row = mysql_fetch_row($wynik)) 
		{
			echo " <option value=\"".$row[0]."\">".$row[1]." ".$row[1]."</option>";
		}
		
		echo "</select>
			<input type=\"submit\" value=\"Usuń\" name=\"usun1\">
		</form> ";
	}
		
	if (isset($_POST['usun1']))
	{
		$profil2_ID = $_POST['nauczyciel_usun'];
		$zapytanie = "delete from nauczyciel where profil_ID =".$profil2_ID."";
		$wynik = mysql_query($zapytanie);
		
		$zapytanie2 = "delete from profil where profil_ID =".$profil2_ID."";
		$wynik2 = mysql_query($zapytanie2);	
		if(!$wynik2)
		{
			echo" Błąd, spróbuj ponownie";
		}
		else
		{
			echo "Zakończono usówanie użytkownika z powodzeniem";
		}		
	}
		
	if (isset($_POST['Dalej4']) && $_POST['prawa2'] == 2)
	{
		echo"<form method=\"POST\" action=\"funkcje.php?akcja=usun_user\">
			Imię i nazwisko: <select name=\"dyrek_usun\" size=\"1\" >";
		
		$zapytanie = "SELECT profil_ID, imie, nazwisko from szkola.profil where prawa = 'dyrektor';";
		$wynik = mysql_query($zapytanie);	
		while ($row = mysql_fetch_row($wynik)) 
		{
			echo " <option value=\"".$row[0]."\">".$row[1]." ".$row[1]."</option>";
		}
		
		echo"</select>
			<input type=\"submit\" value=\"Usuń\" name=\"usun2\">
		</form> ";
	}
		
	if (isset($_POST['usun2']))
	{
		$profil3_ID = $_POST['dyrek_usun'];
		
		$zapytanie2 = "delete from profil where profil_ID =".$profil3_ID."";
		$wynik2 = mysql_query($zapytanie2);	
		if(!$wynik2)
		{
			echo" Błąd, spróbuj ponownie";
		}
		else
		{
			echo "Zakończono usówanie użytkownika z powodzeniem";
		}		
	}
		
	if (isset($_POST['Dalej4']) && $_POST['prawa2'] == 3)
	{
		echo"<form method=\"POST\" action=\"funkcje.php?akcja=usun_user\">
			Imię i nazwisko: <select name=\"planista_usun\" size=\"1\" >";
		
		$zapytanie = "SELECT profil_ID, imie, nazwisko from szkola.profil where prawa = 'planista';";
		$wynik = mysql_query($zapytanie);	
		while ($row = mysql_fetch_row($wynik)) 
		{
			echo " <option value=\"".$row[0]."\">".$row[1]." ".$row[1]."</option>";
		}
		
		echo "</select>
			<input type=\"submit\" value=\"Usuń\" name=\"usun3\">
		</form> ";
	}
		
	if (isset($_POST['usun3']))
	{
		$profil3_ID = $_POST['planista_usun'];
		
		$zapytanie2 = "delete from profil where profil_ID =".$profil3_ID."";
		$wynik2 = mysql_query($zapytanie2);	
		if(!$wynik2)
		{
			echo" Błąd, spróbuj ponownie";
		}
		else
		{
			echo "Zakończono usówanie użytkownika z powodzeniem";
		}		
	}
	
	echo '</div>';
}

?>