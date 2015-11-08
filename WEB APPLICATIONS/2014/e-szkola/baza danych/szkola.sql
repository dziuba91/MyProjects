-- phpMyAdmin SQL Dump
-- version 2.8.2.4
-- http://www.phpmyadmin.net
-- 
-- Host: localhost
-- Czas wygenerowania: 16 Maj 2014, 01:22
-- Wersja serwera: 5.0.24
-- Wersja PHP: 5.2.17
-- 
-- Baza danych: `szkola`
-- 

-- --------------------------------------------------------

-- 
-- Struktura tabeli dla  `klasa`
-- 

CREATE TABLE `klasa` (
  `klasa_ID` int(10) NOT NULL,
  `poziom` int(1) NOT NULL,
  `podklasa` varchar(1) collate ascii_bin NOT NULL,
  `wychowawca_ID` int(10) NOT NULL,
  PRIMARY KEY  (`klasa_ID`),
  KEY `wychowawca_ID` (`wychowawca_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=ascii COLLATE=ascii_bin;

-- 
-- Zrzut danych tabeli `klasa`
-- 

INSERT INTO `klasa` (`klasa_ID`, `poziom`, `podklasa`, `wychowawca_ID`) VALUES (1, 1, 'A', 7),
(2, 1, 'B', 2),
(3, 2, 'A', 3),
(4, 2, 'B', 4),
(5, 3, 'A', 5),
(6, 3, 'B', 6);

-- --------------------------------------------------------

-- 
-- Struktura tabeli dla  `konto_rodzica`
-- 

CREATE TABLE `konto_rodzica` (
  `konto_rodzica_ID` int(10) NOT NULL,
  `profil_ID` int(10) NOT NULL,
  PRIMARY KEY  (`konto_rodzica_ID`),
  KEY `profil_ID` (`profil_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=ascii COLLATE=ascii_bin;

-- 
-- Zrzut danych tabeli `konto_rodzica`
-- 

INSERT INTO `konto_rodzica` (`konto_rodzica_ID`, `profil_ID`) VALUES (1, 23),
(2, 30),
(3, 31),
(4, 32),
(5, 33),
(6, 34);

-- --------------------------------------------------------

-- 
-- Struktura tabeli dla  `nauczyciel`
-- 

CREATE TABLE `nauczyciel` (
  `nauczyciel_ID` int(10) NOT NULL,
  `przedmiot_ID` int(10) NOT NULL,
  `profil_ID` int(10) NOT NULL,
  PRIMARY KEY  (`nauczyciel_ID`),
  KEY `przedmiot_ID` (`przedmiot_ID`),
  KEY `profil_ID` (`profil_ID`),
  KEY `przedmiot_ID_2` (`przedmiot_ID`),
  KEY `profil_ID_2` (`profil_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_polish_ci;

-- 
-- Zrzut danych tabeli `nauczyciel`
-- 

INSERT INTO `nauczyciel` (`nauczyciel_ID`, `przedmiot_ID`, `profil_ID`) VALUES (0, 2, 2),
(1, 1, 2),
(2, 2, 3),
(3, 3, 4),
(4, 4, 5),
(5, 5, 6),
(6, 6, 7),
(7, 7, 8),
(8, 8, 9),
(9, 9, 10),
(10, 10, 11),
(11, 11, 12),
(12, 12, 13),
(13, 13, 14),
(14, 14, 15),
(15, 15, 16),
(16, 16, 17),
(17, 17, 18),
(18, 18, 19),
(19, 19, 20),
(20, 20, 21);

-- --------------------------------------------------------

-- 
-- Struktura tabeli dla  `obecnosci`
-- 

CREATE TABLE `obecnosci` (
  `uczen_ID` int(10) NOT NULL,
  `klasa_ID` int(10) NOT NULL,
  `przedmiot_ID` int(10) NOT NULL,
  `lekcja_ID` int(10) NOT NULL,
  `status_ID` int(2) NOT NULL,
  KEY `uczen_ID` (`uczen_ID`),
  KEY `przedmiot_ID` (`przedmiot_ID`),
  KEY `lekcja_ID` (`lekcja_ID`),
  KEY `status_ID` (`status_ID`),
  KEY `klasa_ID` (`klasa_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=ascii COLLATE=ascii_bin;

-- 
-- Zrzut danych tabeli `obecnosci`
-- 

INSERT INTO `obecnosci` (`uczen_ID`, `klasa_ID`, `przedmiot_ID`, `lekcja_ID`, `status_ID`) VALUES (1, 1, 2, 1, 1),
(2, 1, 2, 1, 2),
(3, 1, 2, 1, 1),
(4, 1, 2, 1, 1),
(5, 1, 2, 1, 3),
(6, 1, 2, 1, 2),
(1, 1, 2, 2, 1),
(2, 1, 2, 2, 1),
(3, 1, 2, 2, 1),
(4, 1, 2, 2, 1),
(5, 1, 2, 2, 1),
(6, 1, 2, 2, 1),
(1, 1, 2, 3, 1),
(2, 1, 2, 3, 3),
(3, 1, 2, 3, 1),
(4, 1, 2, 3, 1),
(5, 1, 2, 3, 4),
(6, 1, 2, 3, 5),
(1, 1, 2, 4, 1),
(2, 1, 2, 4, 2),
(3, 1, 2, 4, 2),
(4, 1, 2, 4, 2),
(5, 1, 2, 4, 1),
(6, 1, 2, 4, 1);

-- --------------------------------------------------------

-- 
-- Struktura tabeli dla  `oceny`
-- 

CREATE TABLE `oceny` (
  `ocena_ID` int(10) NOT NULL,
  `uczen_ID` int(10) NOT NULL,
  `typ_oceny_ID` int(2) default NULL,
  `rodzaj_oceny_ID` int(10) NOT NULL,
  PRIMARY KEY  (`ocena_ID`),
  KEY `uczen_ID` (`uczen_ID`),
  KEY `rodzaj_oceny_ID` (`rodzaj_oceny_ID`),
  KEY `typ_oceny_ID` (`typ_oceny_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=ascii COLLATE=ascii_bin;

-- 
-- Zrzut danych tabeli `oceny`
-- 

INSERT INTO `oceny` (`ocena_ID`, `uczen_ID`, `typ_oceny_ID`, `rodzaj_oceny_ID`) VALUES (1, 1, 5, 1),
(2, 2, 7, 1),
(3, 3, 1, 1),
(4, 4, 8, 1),
(5, 5, NULL, 1),
(6, 6, 4, 1),
(7, 1, 9, 2),
(8, 2, NULL, 2),
(9, 3, 6, 2),
(10, 4, NULL, 2),
(11, 5, NULL, 2),
(12, 6, NULL, 2),
(13, 1, 5, 3),
(14, 2, 5, 3),
(15, 3, 12, 3),
(16, 4, 5, 3),
(17, 5, NULL, 3),
(18, 6, 8, 3);

-- --------------------------------------------------------

-- 
-- Struktura tabeli dla  `plan_lekcji`
-- 

CREATE TABLE `plan_lekcji` (
  `sala_ID` int(10) NOT NULL,
  `godzina_lekcyjna` int(2) NOT NULL,
  `nauczyciel_ID` int(10) NOT NULL,
  `klasa_ID` int(10) NOT NULL,
  `przedmiot_ID` int(10) NOT NULL,
  `dzien_tygodnia` varchar(20) collate ascii_bin NOT NULL,
  `id` int(11) NOT NULL auto_increment,
  PRIMARY KEY  (`id`),
  KEY `sala_ID` (`sala_ID`),
  KEY `nauczyciel_ID` (`nauczyciel_ID`),
  KEY `klasa_ID` (`klasa_ID`),
  KEY `przedmiot_ID` (`przedmiot_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=ascii COLLATE=ascii_bin AUTO_INCREMENT=71 ;

-- 
-- Zrzut danych tabeli `plan_lekcji`
-- 

INSERT INTO `plan_lekcji` (`sala_ID`, `godzina_lekcyjna`, `nauczyciel_ID`, `klasa_ID`, `przedmiot_ID`, `dzien_tygodnia`, `id`) VALUES (9, 1, 6, 1, 5, 'Sroda', 37),
(4, 1, 17, 1, 16, 'Wtorek', 38),
(14, 2, 17, 2, 16, 'Wtorek', 39),
(11, 1, 2, 1, 2, 'Poniedzialek', 40),
(13, 1, 4, 1, 3, 'Czwartek', 41),
(20, 1, 5, 1, 4, 'Piatek', 42),
(13, 2, 18, 1, 17, 'Poniedzialek', 43),
(16, 2, 6, 1, 5, 'Wtorek', 44),
(13, 2, 18, 1, 17, 'Sroda', 45),
(13, 2, 4, 1, 3, 'Czwartek', 46),
(20, 2, 5, 1, 4, 'Piatek', 47),
(4, 3, 7, 1, 6, 'Poniedzialek', 48),
(16, 3, 6, 1, 5, 'Wtorek', 49),
(13, 3, 18, 1, 17, 'Sroda', 50),
(12, 3, 20, 1, 19, 'Czwartek', 51),
(5, 3, 16, 1, 15, 'Piatek', 52),
(18, 4, 11, 1, 10, 'Poniedzialek', 53),
(2, 4, 15, 1, 14, 'Wtorek', 54),
(13, 4, 7, 1, 6, 'Czwartek', 56),
(13, 4, 9, 1, 8, 'Piatek', 57),
(18, 5, 10, 1, 9, 'Poniedzialek', 58),
(8, 5, 13, 1, 12, 'Wtorek', 59),
(3, 5, 4, 1, 3, 'Sroda', 60),
(18, 5, 6, 1, 5, 'Czwartek', 61),
(16, 5, 8, 1, 7, 'Piatek', 62),
(18, 6, 10, 1, 9, 'Poniedzialek', 63),
(10, 6, 14, 1, 13, 'Wtorek', 64),
(20, 6, 5, 1, 4, 'Czwartek', 65),
(7, 6, 19, 1, 18, 'Piatek', 66),
(17, 7, 12, 1, 11, 'Wtorek', 67),
(12, 7, 20, 1, 19, 'Piatek', 68),
(8, 4, 6, 1, 5, 'Sroda', 69),
(5, 1, 8, 2, 7, 'Wtorek', 70);

-- --------------------------------------------------------

-- 
-- Struktura tabeli dla  `profil`
-- 

CREATE TABLE `profil` (
  `profil_ID` int(10) NOT NULL,
  `login` varchar(20) collate ascii_bin NOT NULL,
  `haslo` varchar(20) collate ascii_bin NOT NULL,
  `prawa` varchar(20) collate ascii_bin NOT NULL,
  `imie` varchar(20) collate ascii_bin NOT NULL,
  `nazwisko` varchar(20) collate ascii_bin NOT NULL,
  `pesel` int(11) NOT NULL,
  `data_urodzenia` date NOT NULL,
  PRIMARY KEY  (`profil_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=ascii COLLATE=ascii_bin;

-- 
-- Zrzut danych tabeli `profil`
-- 

INSERT INTO `profil` (`profil_ID`, `login`, `haslo`, `prawa`, `imie`, `nazwisko`, `pesel`, `data_urodzenia`) VALUES (1, 'admin', 'admin', 'administrator', 'Tomasz', 'Dziemidowicz', 2147483647, '1991-03-21'),
(2, 'nauczyciel1', 'nauczyciel1', 'nauczyciel', 'Adam', 'Adacki', 2147483647, '1989-03-21'),
(3, 'nauczyciel2', 'nauczyciel2', 'nauczyciel', 'Bartek', 'Bartacki', 2147483647, '1989-03-21'),
(4, 'nauczyciel3', 'nauczyciel3', 'nauczyciel', 'Cezary', 'Cezarski', 2147483647, '1989-03-21'),
(5, 'nauczyciel4', 'nauczyciel4', 'nauczyciel', 'Daniel', 'Danielski', 2147483647, '1989-03-21'),
(6, 'nauczyciel5', 'nauczyciel5', 'nauczyciel', 'Edward', 'Edwarcki', 2147483647, '1989-03-21'),
(7, 'nauczyciel6', 'nauczyciel6', 'nauczyciel', 'Franek', 'Franelski', 2147483647, '1989-03-21'),
(8, 'nauczyciel7', 'nauczyciel7', 'nauczyciel', 'Iza', 'Izyjska', 2147483647, '1989-03-21'),
(9, 'nauczyciel8', 'nauczyciel8', 'nauczyciel', 'Jola', 'Jolska', 2147483647, '1989-03-21'),
(10, 'nauczyciel9', 'nauczyciel9', 'nauczyciel', 'Krzysztof', 'Krzysztowski', 2147483647, '1989-03-21'),
(11, 'nauczyciel10', 'nauczyciel10', 'nauczyciel', 'Lena', 'Lenska', 2147483647, '1989-03-21'),
(12, 'nauczyciel11', 'nauczyciel11', 'nauczyciel', 'Michal', 'Michalski', 2147483647, '1989-03-21'),
(13, 'nauczyciel12', 'nauczyciel12', 'nauczyciel', 'Natalia', 'Natalska', 2147483647, '1989-03-21'),
(14, 'nauczyciel13', 'nauczyciel13', 'nauczyciel', 'Ola', 'Olska', 2147483647, '1989-03-21'),
(15, 'nauczyciel14', 'nauczyciel14', 'nauczyciel', 'Ula', 'Ulska', 2147483647, '1989-03-21'),
(16, 'nauczyciel15', 'nauczyciel15', 'nauczyciel', 'Piotr', 'Piotrski', 2147483647, '1989-03-21'),
(17, 'nauczyciel16', 'nauczyciel16', 'nauczyciel', 'Rafal', 'Rafalski', 2147483647, '1989-03-21'),
(18, 'nauczyciel17', 'nauczyciel17', 'nauczyciel', 'Stefan', 'Stefanski', 2147483647, '1989-03-21'),
(19, 'nauczyciel18', 'nauczyciel18', 'nauczyciel', 'Tomek', 'Tomski', 2147483647, '1989-03-21'),
(20, 'nauczyciel19', 'nauczyciel19', 'nauczyciel', 'Waldemar', 'Waldemarski', 2147483647, '1989-03-21'),
(21, 'nauczyciel20', 'nauczyciel20', 'nauczyciel', 'Zbigniew', 'Zbyszki', 2147483647, '1989-03-21'),
(22, 'uczen1', 'uczen1', 'uczen', 'Adam', 'Adacki', 2147483647, '1989-03-21'),
(23, 'rodzic1', 'rodzic1', 'rodzic', 'Adam', 'Adacki', 2147483647, '1989-03-21'),
(24, 'dyrektor', 'dyrektor', 'dyrektor', 'Dyrektor', 'Dyrektorski', 2147483647, '1989-03-21'),
(25, 'uczen2', 'uczen2', 'uczen', 'Aleksandra', 'Awukiewicz', 960501992, '1996-05-01'),
(26, 'uczen3', 'uczen3', 'uczen', 'Andrzej', 'Adamczuk', 960814992, '1996-08-14'),
(27, 'uczen4', 'uczen4', 'uczen', 'Maciej', 'Michalczuk', 960115992, '1996-01-15'),
(28, 'uczen5', 'uczen5', 'uczen', 'Agnieszka', 'Franek', 961205992, '1996-12-05'),
(29, 'uczen6', 'uczen6', 'uczen', 'Tomasz', 'Krul', 96102199, '1996-10-21'),
(30, 'rodzic2', 'rodzic2', 'rodzic', 'Marcin', 'Awukiewicz', 710501992, '1971-05-01'),
(31, 'rodzic3', 'rodzic3', 'rodzic', 'Elzbieta', 'Adamczuk', 650814992, '1965-08-14'),
(32, 'rodzic4', 'rodzic4', 'rodzic', 'Alojzy', 'Michalczuk', 770115992, '1977-01-15'),
(33, 'rodzic5', 'rodzic5', 'rodzic', 'Piotr', 'Franek', 691205992, '1969-12-05'),
(34, 'rodzic6', 'rodzic6', 'rodzic', 'Karolina', 'Krul', 711021992, '1971-10-21');

-- --------------------------------------------------------

-- 
-- Struktura tabeli dla  `przedmiot`
-- 

CREATE TABLE `przedmiot` (
  `przedmiot_ID` int(10) NOT NULL,
  `nazwa_przedmiotu` varchar(50) collate utf8_bin NOT NULL,
  PRIMARY KEY  (`przedmiot_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- 
-- Zrzut danych tabeli `przedmiot`
-- 

INSERT INTO `przedmiot` (`przedmiot_ID`, `nazwa_przedmiotu`) VALUES (1, 'Matematyka'),
(2, 'Geografia'),
(3, 'WF chlopcy'),
(4, 'WF dziewczeta'),
(5, 'Jezyk Polski'),
(6, 'Jezyk Angielski'),
(7, 'Biologia'),
(8, 'WDZ'),
(9, 'Informatyka'),
(10, 'Muzyka'),
(11, 'GW'),
(12, 'Język Niemiecki'),
(13, 'Historia'),
(14, 'Chemia'),
(15, 'Religia'),
(16, 'Fizyka'),
(17, 'Matematyka'),
(18, 'Zaj. Art.'),
(19, 'WOS'),
(20, 'Plastyka');

-- --------------------------------------------------------

-- 
-- Struktura tabeli dla  `rodzaj_oceny`
-- 

CREATE TABLE `rodzaj_oceny` (
  `rodzaj_oceny_ID` int(10) NOT NULL,
  `lekcja_ID` int(10) default NULL,
  `przedmiot_ID` int(10) NOT NULL,
  `klasa_ID` int(10) NOT NULL,
  `nazwa` varchar(50) collate ascii_bin NOT NULL,
  `ocena_koncowa` int(1) default NULL,
  PRIMARY KEY  (`rodzaj_oceny_ID`),
  KEY `lekcja_ID` (`lekcja_ID`),
  KEY `przedmiot_ID` (`przedmiot_ID`),
  KEY `klasa_ID` (`klasa_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=ascii COLLATE=ascii_bin;

-- 
-- Zrzut danych tabeli `rodzaj_oceny`
-- 

INSERT INTO `rodzaj_oceny` (`rodzaj_oceny_ID`, `lekcja_ID`, `przedmiot_ID`, `klasa_ID`, `nazwa`, `ocena_koncowa`) VALUES (1, 1, 2, 1, 'sprawdzian 1', NULL),
(2, 2, 2, 1, 'pytania 1', NULL),
(3, 1, 2, 1, 'wypracowanie 1', NULL);

-- --------------------------------------------------------

-- 
-- Struktura tabeli dla  `sala`
-- 

CREATE TABLE `sala` (
  `sala_ID` int(10) NOT NULL,
  `numer_sali` int(3) NOT NULL,
  PRIMARY KEY  (`sala_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=ascii COLLATE=ascii_bin;

-- 
-- Zrzut danych tabeli `sala`
-- 

INSERT INTO `sala` (`sala_ID`, `numer_sali`) VALUES (1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(7, 7),
(8, 8),
(9, 9),
(10, 10),
(11, 101),
(12, 102),
(13, 103),
(14, 104),
(15, 105),
(16, 106),
(17, 107),
(18, 108),
(19, 109),
(20, 110);

-- --------------------------------------------------------

-- 
-- Struktura tabeli dla  `system_obecnosci`
-- 

CREATE TABLE `system_obecnosci` (
  `status_ID` int(10) NOT NULL,
  `identyfikator` varchar(2) collate ascii_bin NOT NULL,
  `nazwa` varchar(40) collate ascii_bin NOT NULL,
  PRIMARY KEY  (`status_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=ascii COLLATE=ascii_bin;

-- 
-- Zrzut danych tabeli `system_obecnosci`
-- 

INSERT INTO `system_obecnosci` (`status_ID`, `identyfikator`, `nazwa`) VALUES (1, '', 'obecny'),
(2, 'S', 'spozniony'),
(3, 'N', 'nieobecny'),
(4, 'U', 'usprawiedliwiony'),
(5, 'Z', 'zwolniony');

-- --------------------------------------------------------

-- 
-- Struktura tabeli dla  `system_oceniania`
-- 

CREATE TABLE `system_oceniania` (
  `typ_oceny_ID` int(2) NOT NULL,
  `ocena` varchar(2) collate ascii_bin NOT NULL,
  `slownie` varchar(40) collate ascii_bin NOT NULL,
  `slownie_skrot` varchar(20) collate ascii_bin NOT NULL,
  `waga_oceny` float NOT NULL,
  `ocena_koncowa` int(1) default NULL,
  PRIMARY KEY  (`typ_oceny_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=ascii COLLATE=ascii_bin;

-- 
-- Zrzut danych tabeli `system_oceniania`
-- 

INSERT INTO `system_oceniania` (`typ_oceny_ID`, `ocena`, `slownie`, `slownie_skrot`, `waga_oceny`, `ocena_koncowa`) VALUES (1, '1', 'niedostateczny', 'ndst', 1, 1),
(2, '-2', 'dopuszczajacy minus', '-dop', 1.8, NULL),
(3, '2', 'dopuszczajacy', 'dop', 2, 1),
(4, '+2', 'dopuszczajacy plus', '+dop', 2.5, NULL),
(5, '-3', 'dostateczny minus', '-dst', 2.8, NULL),
(6, '3', 'dostateczny', 'dst', 3, 1),
(7, '+3', 'dostateczny plus', '+dst', 3.5, NULL),
(8, '-4', 'dobry minus', '-db', 3.8, NULL),
(9, '4', 'dobry', 'db', 4, 1),
(10, '+4', 'dobry plus', '+db', 4.5, NULL),
(11, '-5', 'bardzo dobry minus', '-bdb', 4.8, NULL),
(12, '5', 'bardzo dobry', 'bdb', 5, 1),
(13, '+5', 'bardzo dobry plus', '+bdb', 5.5, NULL),
(14, '-6', 'celujacy minus', '-cel', 5.8, NULL),
(15, '6', 'celujacy', 'cel', 6, 1);

-- --------------------------------------------------------

-- 
-- Struktura tabeli dla  `temat_lekcji`
-- 

CREATE TABLE `temat_lekcji` (
  `lekcja_ID` int(10) NOT NULL,
  `przedmiot_ID` int(10) NOT NULL,
  `nauczyciel_ID` int(10) NOT NULL,
  `klasa_id` int(10) NOT NULL,
  `temat_lekcji` varchar(100) collate utf8_bin NOT NULL,
  `data` date NOT NULL,
  PRIMARY KEY  (`lekcja_ID`),
  KEY `przedmiot_ID` (`przedmiot_ID`),
  KEY `nauczyciel_ID` (`nauczyciel_ID`),
  KEY `klasa_id` (`klasa_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- 
-- Zrzut danych tabeli `temat_lekcji`
-- 

INSERT INTO `temat_lekcji` (`lekcja_ID`, `przedmiot_ID`, `nauczyciel_ID`, `klasa_id`, `temat_lekcji`, `data`) VALUES (1, 2, 2, 1, 'Zajęcia wstępne.', '2014-02-11'),
(2, 2, 2, 1, 'Demografia Japonii.', '2014-02-18'),
(3, 2, 2, 1, 'Ludność w Japonii.', '2014-02-25'),
(4, 2, 2, 1, 'Ukształtowanie terenu Japonii.', '2014-03-02');

-- --------------------------------------------------------

-- 
-- Struktura tabeli dla  `uczen`
-- 

CREATE TABLE `uczen` (
  `uczen_ID` int(10) NOT NULL,
  `imię_matki` varchar(20) collate utf8_bin NOT NULL,
  `imię_ojca` varchar(20) collate utf8_bin NOT NULL,
  `klasa_ID` int(10) NOT NULL,
  `konto_rodzica_ID` int(10) NOT NULL,
  `profil_ID` int(10) NOT NULL,
  PRIMARY KEY  (`uczen_ID`),
  KEY `konto_rodzica_ID` (`konto_rodzica_ID`),
  KEY `profil_ID` (`profil_ID`),
  KEY `konto_rodzica_ID_2` (`konto_rodzica_ID`),
  KEY `profil_ID_2` (`profil_ID`),
  KEY `klasa_ID` (`klasa_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- 
-- Zrzut danych tabeli `uczen`
-- 

INSERT INTO `uczen` (`uczen_ID`, `imię_matki`, `imię_ojca`, `klasa_ID`, `konto_rodzica_ID`, `profil_ID`) VALUES (1, 'Kasia', 'Zbyszek', 1, 1, 22),
(2, 'Anna', 'Marcin', 1, 2, 25),
(3, 'Elzbieta', 'Tomasz', 1, 3, 26),
(4, 'Franciszka', 'Alojzy', 1, 4, 27),
(5, 'Zuzanna', 'Piotr', 1, 5, 28),
(6, 'Karolina', 'Maciej', 1, 6, 29);

-- --------------------------------------------------------

-- 
-- Struktura tabeli dla  `wiadomosc`
-- 

CREATE TABLE `wiadomosc` (
  `wiadomosc_ID` int(11) NOT NULL auto_increment,
  `odbiorca_ID` int(10) NOT NULL,
  `nadawca_ID` int(10) NOT NULL,
  `tresc` longtext collate utf8_bin NOT NULL,
  `temat` varchar(100) collate utf8_bin NOT NULL,
  `nowa` int(1) NOT NULL,
  `czas` timestamp NOT NULL default CURRENT_TIMESTAMP,
  PRIMARY KEY  (`wiadomosc_ID`),
  KEY `nadawca_ID` (`nadawca_ID`),
  KEY `odbiorca_ID` (`odbiorca_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin AUTO_INCREMENT=88 ;

-- 
-- Zrzut danych tabeli `wiadomosc`
-- 

INSERT INTO `wiadomosc` (`wiadomosc_ID`, `odbiorca_ID`, `nadawca_ID`, `tresc`, `temat`, `nowa`, `czas`) VALUES (7, 2, 2, 'Linijka1.\r\nLinijka2.', 'asf', 0, '2014-04-06 19:43:06'),
(9, 2, 2, 'a a a a a a a aa a a aa', 'as', 0, '2014-04-06 19:57:01'),
(10, 2, 2, 'a a a a a a a a a a a a a a a a a a a a a s s  ds f f a  d sf  daf adg ag a g adg g a dgsfg ', 'asd', 0, '2014-04-06 19:57:40'),
(11, 2, 2, 'text text text text text text text text text', 'asf', 0, '2014-04-06 19:58:16'),
(12, 2, 2, 'text\r\ntext text\r\ntext text text.', 'saf', 0, '2014-04-06 19:58:35'),
(13, 2, 1, 'rrr', 'info', 0, '2014-04-07 23:57:00'),
(14, 2, 3, 'w2', 'aa', 0, '2014-04-08 00:00:11'),
(15, 3, 3, 'w2', 'aa', 0, '2014-04-08 00:00:11'),
(16, 4, 3, 'w2', 'aa', 0, '2014-04-08 00:00:12'),
(17, 5, 3, 'w2', 'aa', 1, '2014-04-08 00:00:12'),
(18, 6, 3, 'w2', 'aa', 1, '2014-04-08 00:00:12'),
(19, 7, 3, 'w2', 'aa', 1, '2014-04-08 00:00:12'),
(20, 8, 3, 'w2', 'aa', 1, '2014-04-08 00:00:12'),
(21, 9, 3, 'w2', 'aa', 1, '2014-04-08 00:00:12'),
(22, 10, 3, 'w2', 'aa', 1, '2014-04-08 00:00:12'),
(23, 11, 3, 'w2', 'aa', 1, '2014-04-08 00:00:12'),
(24, 12, 3, 'w2', 'aa', 1, '2014-04-08 00:00:12'),
(25, 13, 3, 'w2', 'aa', 1, '2014-04-08 00:00:12'),
(26, 14, 3, 'w2', 'aa', 1, '2014-04-08 00:00:12'),
(27, 15, 3, 'w2', 'aa', 1, '2014-04-08 00:00:12'),
(28, 16, 3, 'w2', 'aa', 1, '2014-04-08 00:00:12'),
(29, 17, 3, 'w2', 'aa', 1, '2014-04-08 00:00:12'),
(30, 18, 3, 'w2', 'aa', 0, '2014-04-08 00:00:12'),
(31, 19, 3, 'w2', 'aa', 1, '2014-04-08 00:00:12'),
(32, 20, 3, 'w2', 'aa', 1, '2014-04-08 00:00:12'),
(33, 21, 3, 'w2', 'aa', 0, '2014-04-08 00:00:12'),
(34, 2, 1, 'fadf', 'fa', 0, '2014-04-08 00:03:37'),
(43, 2, 1, 'fdafds', 'df', 0, '2014-04-12 17:09:29'),
(44, 3, 1, 'fdafds', 'df', 0, '2014-04-12 17:09:30'),
(45, 4, 1, 'fdafds', 'df', 0, '2014-04-12 17:09:30'),
(46, 5, 1, 'fdafds', 'df', 1, '2014-04-12 17:09:30'),
(47, 6, 1, 'fdafds', 'df', 1, '2014-04-12 17:09:30'),
(48, 7, 1, 'fdafds', 'df', 1, '2014-04-12 17:09:30'),
(49, 8, 1, 'fdafds', 'df', 1, '2014-04-12 17:09:30'),
(50, 9, 1, 'fdafds', 'df', 1, '2014-04-12 17:09:30'),
(51, 10, 1, 'fdafds', 'df', 1, '2014-04-12 17:09:30'),
(52, 11, 1, 'fdafds', 'df', 1, '2014-04-12 17:09:30'),
(53, 12, 1, 'fdafds', 'df', 1, '2014-04-12 17:09:30'),
(54, 13, 1, 'fdafds', 'df', 1, '2014-04-12 17:09:30'),
(55, 14, 1, 'fdafds', 'df', 1, '2014-04-12 17:09:30'),
(56, 15, 1, 'fdafds', 'df', 1, '2014-04-12 17:09:30'),
(57, 16, 1, 'fdafds', 'df', 1, '2014-04-12 17:09:30'),
(58, 17, 1, 'fdafds', 'df', 1, '2014-04-12 17:09:30'),
(59, 18, 1, 'fdafds', 'df', 0, '2014-04-12 17:09:30'),
(60, 19, 1, 'fdafds', 'df', 1, '2014-04-12 17:09:30'),
(61, 20, 1, 'fdafds', 'df', 1, '2014-04-12 17:09:30'),
(62, 21, 1, 'fdafds', 'df', 1, '2014-04-12 17:09:30'),
(63, 1, 4, 'chelsea dno', 'chelsea dno', 0, '2014-05-04 15:19:08'),
(64, 1, 1, 'whats up', 'whats up', 0, '2014-05-04 18:56:55'),
(65, 2, 1, 'whats up', 'whats up', 0, '2014-05-04 18:56:55'),
(66, 3, 1, 'whats up', 'whats up', 1, '2014-05-04 18:56:55'),
(67, 4, 1, 'whats up', 'whats up', 1, '2014-05-04 18:56:55'),
(68, 5, 1, 'whats up', 'whats up', 1, '2014-05-04 18:56:55'),
(69, 6, 1, 'whats up', 'whats up', 1, '2014-05-04 18:56:55'),
(70, 7, 1, 'whats up', 'whats up', 1, '2014-05-04 18:56:55'),
(71, 8, 1, 'whats up', 'whats up', 1, '2014-05-04 18:56:55'),
(72, 9, 1, 'whats up', 'whats up', 1, '2014-05-04 18:56:55'),
(73, 10, 1, 'whats up', 'whats up', 1, '2014-05-04 18:56:55'),
(74, 11, 1, 'whats up', 'whats up', 1, '2014-05-04 18:56:55'),
(75, 12, 1, 'whats up', 'whats up', 1, '2014-05-04 18:56:55'),
(76, 13, 1, 'whats up', 'whats up', 1, '2014-05-04 18:56:55'),
(77, 14, 1, 'whats up', 'whats up', 1, '2014-05-04 18:56:55'),
(78, 15, 1, 'whats up', 'whats up', 1, '2014-05-04 18:56:55'),
(79, 16, 1, 'whats up', 'whats up', 1, '2014-05-04 18:56:55'),
(80, 17, 1, 'whats up', 'whats up', 1, '2014-05-04 18:56:55'),
(81, 18, 1, 'whats up', 'whats up', 1, '2014-05-04 18:56:55'),
(82, 19, 1, 'whats up', 'whats up', 1, '2014-05-04 18:56:55'),
(83, 20, 1, 'whats up', 'whats up', 1, '2014-05-04 18:56:55'),
(84, 21, 1, 'whats up', 'whats up', 1, '2014-05-04 18:56:55'),
(85, 22, 1, 'whats up', 'whats up', 0, '2014-05-04 18:56:55'),
(86, 23, 1, 'whats up', 'whats up', 1, '2014-05-04 18:56:55'),
(87, 24, 1, 'whats up', 'whats up', 1, '2014-05-04 18:56:55');

-- 
-- Ograniczenia dla zrzutów tabel
-- 

-- 
-- Ograniczenia dla tabeli `klasa`
-- 
ALTER TABLE `klasa`
  ADD CONSTRAINT `klasa_ibfk_1` FOREIGN KEY (`wychowawca_ID`) REFERENCES `profil` (`profil_ID`);

-- 
-- Ograniczenia dla tabeli `konto_rodzica`
-- 
ALTER TABLE `konto_rodzica`
  ADD CONSTRAINT `konto_rodzica_ibfk_1` FOREIGN KEY (`profil_ID`) REFERENCES `profil` (`profil_ID`);

-- 
-- Ograniczenia dla tabeli `nauczyciel`
-- 
ALTER TABLE `nauczyciel`
  ADD CONSTRAINT `nauczyciel_ibfk_1` FOREIGN KEY (`przedmiot_ID`) REFERENCES `przedmiot` (`przedmiot_ID`),
  ADD CONSTRAINT `nauczyciel_ibfk_2` FOREIGN KEY (`profil_ID`) REFERENCES `profil` (`profil_ID`);

-- 
-- Ograniczenia dla tabeli `obecnosci`
-- 
ALTER TABLE `obecnosci`
  ADD CONSTRAINT `obecnosci_ibfk_34` FOREIGN KEY (`uczen_ID`) REFERENCES `uczen` (`uczen_ID`),
  ADD CONSTRAINT `obecnosci_ibfk_35` FOREIGN KEY (`klasa_ID`) REFERENCES `klasa` (`klasa_ID`),
  ADD CONSTRAINT `obecnosci_ibfk_36` FOREIGN KEY (`przedmiot_ID`) REFERENCES `przedmiot` (`przedmiot_ID`),
  ADD CONSTRAINT `obecnosci_ibfk_37` FOREIGN KEY (`lekcja_ID`) REFERENCES `temat_lekcji` (`lekcja_ID`),
  ADD CONSTRAINT `obecnosci_ibfk_38` FOREIGN KEY (`status_ID`) REFERENCES `system_obecnosci` (`status_ID`);

-- 
-- Ograniczenia dla tabeli `oceny`
-- 
ALTER TABLE `oceny`
  ADD CONSTRAINT `oceny_ibfk_3` FOREIGN KEY (`uczen_ID`) REFERENCES `uczen` (`uczen_ID`),
  ADD CONSTRAINT `oceny_ibfk_4` FOREIGN KEY (`typ_oceny_ID`) REFERENCES `system_oceniania` (`typ_oceny_ID`),
  ADD CONSTRAINT `oceny_ibfk_5` FOREIGN KEY (`rodzaj_oceny_ID`) REFERENCES `rodzaj_oceny` (`rodzaj_oceny_ID`);

-- 
-- Ograniczenia dla tabeli `plan_lekcji`
-- 
ALTER TABLE `plan_lekcji`
  ADD CONSTRAINT `plan_lekcji_ibfk_1` FOREIGN KEY (`sala_ID`) REFERENCES `sala` (`sala_ID`),
  ADD CONSTRAINT `plan_lekcji_ibfk_2` FOREIGN KEY (`nauczyciel_ID`) REFERENCES `nauczyciel` (`nauczyciel_ID`),
  ADD CONSTRAINT `plan_lekcji_ibfk_3` FOREIGN KEY (`klasa_ID`) REFERENCES `klasa` (`klasa_ID`),
  ADD CONSTRAINT `plan_lekcji_ibfk_4` FOREIGN KEY (`przedmiot_ID`) REFERENCES `przedmiot` (`przedmiot_ID`);

-- 
-- Ograniczenia dla tabeli `rodzaj_oceny`
-- 
ALTER TABLE `rodzaj_oceny`
  ADD CONSTRAINT `rodzaj_oceny_ibfk_1` FOREIGN KEY (`lekcja_ID`) REFERENCES `temat_lekcji` (`lekcja_ID`),
  ADD CONSTRAINT `rodzaj_oceny_ibfk_2` FOREIGN KEY (`przedmiot_ID`) REFERENCES `przedmiot` (`przedmiot_ID`),
  ADD CONSTRAINT `rodzaj_oceny_ibfk_3` FOREIGN KEY (`klasa_ID`) REFERENCES `klasa` (`klasa_ID`);

-- 
-- Ograniczenia dla tabeli `temat_lekcji`
-- 
ALTER TABLE `temat_lekcji`
  ADD CONSTRAINT `temat_lekcji_ibfk_5` FOREIGN KEY (`przedmiot_ID`) REFERENCES `przedmiot` (`przedmiot_ID`),
  ADD CONSTRAINT `temat_lekcji_ibfk_6` FOREIGN KEY (`nauczyciel_ID`) REFERENCES `nauczyciel` (`nauczyciel_ID`),
  ADD CONSTRAINT `temat_lekcji_ibfk_7` FOREIGN KEY (`klasa_id`) REFERENCES `klasa` (`klasa_ID`);

-- 
-- Ograniczenia dla tabeli `uczen`
-- 
ALTER TABLE `uczen`
  ADD CONSTRAINT `uczen_ibfk_2` FOREIGN KEY (`konto_rodzica_ID`) REFERENCES `konto_rodzica` (`konto_rodzica_ID`),
  ADD CONSTRAINT `uczen_ibfk_3` FOREIGN KEY (`profil_ID`) REFERENCES `profil` (`profil_ID`),
  ADD CONSTRAINT `uczen_ibfk_4` FOREIGN KEY (`klasa_ID`) REFERENCES `klasa` (`klasa_ID`);

-- 
-- Ograniczenia dla tabeli `wiadomosc`
-- 
ALTER TABLE `wiadomosc`
  ADD CONSTRAINT `wiadomosc_ibfk_1` FOREIGN KEY (`odbiorca_ID`) REFERENCES `profil` (`profil_ID`),
  ADD CONSTRAINT `wiadomosc_ibfk_2` FOREIGN KEY (`nadawca_ID`) REFERENCES `profil` (`profil_ID`);
