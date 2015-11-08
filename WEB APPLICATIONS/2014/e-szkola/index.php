<?php
	session_start();
	include 'baza.php';
?>

<html>
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
		<script type="text/javascript" src="skrypty.js"></script>
		<link rel="stylesheet" type="text/css" href="style.css" />
	</head>
	<body>
		<form method="POST" action="index.php">
			Login: <input type="text" name="login">
			Haslo: <input type="password" name="haslo">
			<input type="submit" value="Zaloguj" name="loguj">
		</form>
<?php	
	echo '<a href="rejestracja.php">Rejestracja otwarta.</a></br>';
	
	if (isset($_POST['loguj']))
	{
		$login =mysql_real_escape_string($_POST['login']);
		$haslo =mysql_real_escape_string($_POST['haslo']);
		$wynik=mysql_query("SELECT profil_ID FROM szkola.profil WHERE login = '".$login."' AND haslo = '".$haslo."';");

		if (mysql_num_rows($wynik) > 0)
		{
			while ($row = mysql_fetch_row($wynik))
			$_SESSION['id'] = $row[0];
			mysql_close();
			header("Location: funkcje.php?akcja=strona_glowna"); 
		}
		else 
		{
			mysql_close();
			echo "Wpisano zle dane";
			session_destroy();
		}
	}
?>
	</body>
</html>