<?php
function zmien_ustawienia()
{
?>
	
	<div id="container">
	<form method="POST" action="funkcje.php?akcja=zmiana">
		<table class="pusta"><tr><td>
			stary login:</td><td> <input type="text" name="login_s"></td></tr><tr><td>
			nowy login:</td><td> <input type="text" name="login_n"></td></tr><tr><td>
			potwierdź nowy login: </td><td><input type="text" name="login_p"></td></tr></table>
		<center><input type="submit" value="Zmień login" name="zmien_login"></center>
	</form>
	
	<form method="POST" action="funkcje.php?akcja=zmiana">
		<table class="pusta"><tr><td>
			stare hasło:</td><td> <input type="password" name="haslo_s"></td></tr><tr><td>
			nowe hasło:</td><td> <input type="password" name="haslo_n"></td></tr><tr><td>
			potwierdź nowe hasło: </td><td><input type="password" name="haslo_p"></td></tr></table>
		<center><input type="submit" value="Zmień hasło" name="zmien_haslo"></center>
	</form>
	
<?php
	if (isset($_POST['zmien_login']))
	{
		if ($_POST['login_s']!='' && $_POST['login_n']!='' && $_POST['login_p']!='')
		{
			if ($_POST['login_n']==$_POST['login_p'])
			{
				$zapytanie="select login from profil where login='".$_POST['login_n']."'";
				$wynik = mysql_query($zapytanie);
				if (mysql_num_rows($wynik) == 0)
				{
					$zapytanie="select login from profil where profil_ID=".$_SESSION['id']."";
					$wynik = mysql_query($zapytanie);
					
					if ($row = mysql_fetch_row($wynik)) $login=$row[0];
					
					if ($login==$_POST['login_s'])
					{
						$zapytanie="update profil set login='".$_POST['login_n']."' where profil_ID=".$_SESSION['id']."";
						$wynik = mysql_query($zapytanie);
						echo '<h1>Ustawiono nowy login!</h1>';
					}
					else
						echo '<h1>Niepoprawny stary login!</h1>';
				}
				else
					echo '<h1>Login w użyciu, wybierz inny!</h1>';
			}
			else
				echo '<h1>Nowy login i potwierdzenie się nie zgadzają!</h1>';
		}
		else
			echo '<h1>Wypełnij wszystkie pola!</h1>';
	}
	
	if (isset($_POST['zmien_haslo']))
	{
		if ($_POST['haslo_s']!='' && $_POST['haslo_n']!='' && $_POST['haslo_p']!='')
		{
			if ($_POST['haslo_n']==$_POST['haslo_p'])
			{
				$zapytanie="select haslo from profil where profil_ID=".$_SESSION['id']."";
				$wynik = mysql_query($zapytanie);
				if ($row = mysql_fetch_row($wynik))
					$haslo=$row[0];
				if ($haslo==$_POST['haslo_s'])
				{
					$zapytanie="update profil set haslo='".$_POST['haslo_n']."' where profil_ID=".$_SESSION['id']."";
					$wynik = mysql_query($zapytanie);
					echo '<h1>Ustawiono nowe hasło!</h1>';
				}
				else
					echo '<h1>Niepoprawne stare hasło!</h1>';
			}
			else
				echo '<h1>Nowe hasło i potwierdzenie się nie zgadzają!</h1>';
		}
		else
			echo '<h1>Wypełnij wszystkie pola!</h1>';
	}
}
?>