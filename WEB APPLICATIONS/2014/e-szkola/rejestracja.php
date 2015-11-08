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
		<a href="index.php"><< Powrót</a></br></br>
		<form method="POST" action="rejestracja.php">
		<table class="pusta"><tr><td>
				Imię:</td><td> <input type="text" name="imie">
			</td><td>
				Login:</td><td> <input type="text" name="login"></td></tr><tr><td>
				Nazwisko: </td><td><input type="text" name="nazwisko"></td><td>
				Hasło: </td><td><input type="password" name="haslo"></td><td></tr><tr><td>
				Data urodzenia: </td><td> R:
			<select display: inline-block name="rok">
			<option value=""></option>
	<?php
		for($i=date("Y")-20;$i<date("Y")-5;$i++)
			echo '<option value="'.$i.'">'.$i.'</option>';
	?>
			</select>
			M: <select display: inline-block name="miesiac">
			<option value=""></option>
	<?php
		for($i=1;$i<13;$i++)
		{
			$j=$i;
			if ($i<10)
				$j='0'.$i;
			
			echo '<option value="'.$j.'">'.$i.'</option>';
		}
	?>
	
			</select>
			D: <select display: inline-block name="dzien">
			<option value=""></option>
	<?php
		for($i=1;$i<32;$i++)
		{
			$j=$i;
			
			if ($i<10)
				$j='0'.$i;
	
			echo '<option value="'.$j.'">'.$i.'</option>';
		}
	?>
	
			</select>
		</td><td>
			Potwierdź hasło: </td><td><input type="password" name="haslo2"></td><td></tr><tr><td>
			Imię matki: </td><td><input type="text" name="imie_m"></td><td>
			Pesel: </td><td><input type="number" maxlength="11" name="pesel" onkeypress="return isNumberKey(event)"></td><td></tr><tr><td>
			Imię ojca: </td><td><input type="text" name="imie_o"></td>
		</tr></table>
	
		<center><input type="submit" value="Zarejestruj" name="rej"></center>
	</form>
	
	<?php	
		if (isset($_POST['rej']))
		{
			$imie = mysql_real_escape_string($_POST['imie']);
			$nazw = mysql_real_escape_string($_POST['nazwisko']);
			$login = mysql_real_escape_string($_POST['login']);
			$haslo = mysql_real_escape_string($_POST['haslo']);
			$haslo_spr = mysql_real_escape_string($_POST['haslo2']);
			$data = $_POST['rok'].'-'.$_POST['miesiac'].'-'.$_POST['dzien'];
			$matka = mysql_real_escape_string($_POST['imie_m']);
			$ojciec = mysql_real_escape_string($_POST['imie_o']);
			$pesel = mysql_real_escape_string($_POST['pesel']);
			
			if ($imie != '' && $nazw != '' && $imie != '' && $login != '' && $haslo != '' && $haslo_spr != '' && $data != '--' 
				&& $matka != '' && $ojciec != '' && $pesel !='')
			{
				if ($haslo == $haslo_spr)
				{
					$zapytanie = "select login from profil where login='".$login."'";
					$wynik = mysql_query($zapytanie);
					
					if (mysql_num_rows($wynik) == 0)
					{ 
						echo '
							</br></br></br>
							<h2>Sprawdź dokładnie przedstawione niżej dane i potwierdź jeżeli się zgadzają. W celu dokonania ewentualnych korekt proszę wypełnić jeszcze raz formularz. </h2>
						<form method="POST" action="rejestracja.php">
							<center><table class="pusta2"><tr><td>
								Imię:</td><td><b>'.$imie.'</b></td><td>
								Login:</td><td><b>'.$login.'</b></td></tr><tr><td>
								Nazwisko: </td><td><b>'.$nazw.'</b></td><td>
								Haslo: </td><td><b>'.$haslo.'</b></td><td></tr><tr><td>
								Data urodzenia:</td><td><b>'.$data.'</b></td><td>
								Pesel: </td><td><b>'.$pesel.'</b></td><td></tr><tr><td>
								Imię matki: </td><td><b>'.$matka.'</b></td><td>	
								Imię ojca: </td><td><b>'.$ojciec.'</b></td>
							</tr></table></center>
							
							<input style="display:none;" type="text" name="imie" value="'.$imie.'">
							<input style="display:none;" type="text" name="login" value="'.$login.'">	
							<input style="display:none;" type="text" name="nazw" value="'.$nazw.'">	
							<input style="display:none;" type="text" name="haslo" value="'.$haslo.'">	
							<input style="display:none;" type="text" name="data" value="'.$data.'">	
							<input style="display:none;" type="text" name="pesel" value="'.$pesel.'">	
							<input style="display:none;" type="text" name="matka" value="'.$matka.'">
							<input style="display:none;" type="text" name="ojciec" value="'.$ojciec.'">						
							<center><input type="submit" value="Potwierdź" name="potwierdz"></center>
						</form>';
					}
					else
						echo "<center><b>Login w użyciu, wybierz inny!</b></center>";
				}
				else
					echo "<center><b>Hasło i potwierdzenie się nie zgadzają!</b></center>";
			}
			else 
				echo "<center><b>Wypełnij wszystkie pola!</b></center>";
		}
		
		if (isset($_POST['potwierdz']))
		{
			$imie = $_POST['imie'];
			$nazw = $_POST['nazw'];
			$login = $_POST['login'];
			$haslo = $_POST['haslo'];
			$data = $_POST['data'];
			$pesel = $_POST['pesel'];
			$matka = $_POST['matka'];
			$ojciec = $_POST['ojciec'];
			
			$zapytanie = "insert into profil (`login`, `haslo`, `prawa`, `imie`, `nazwisko`, `pesel`, `data_urodzenia`) values
				('".$login."', '".$haslo."', 'uczen', '".$imie."', '".$nazw."', '".$pesel."', '".$data."')";
			$wynik = mysql_query($zapytanie);
			$zapytanie = "select profil_ID from profil where login='".$login."'";
			$wynik = mysql_query($zapytanie);
			while ($row = mysql_fetch_row($wynik))
				$profil_id=$row[0];
			
			$zapytanie = "insert into uczen (`imię_matki`, `imię_ojca`, `profil_ID`, `status`) values
				('".$matka."', '".$ojciec."', '".$profil_id."', 0)";
			$wynik = mysql_query($zapytanie);
			echo'<h2>Utworzono konto!</h2>';
			echo '<h1>Śledź proces rektutacji logując się na utworzone właśnie konto.</h1>';
		}
	?>
	
	</body>
</html>