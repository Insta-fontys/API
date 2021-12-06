-- phpMyAdmin SQL Dump
-- version 5.0.4
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Gegenereerd op: 06 dec 2021 om 12:31
-- Serverversie: 10.4.17-MariaDB
-- PHP-versie: 8.0.0

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `mygram`
--
CREATE DATABASE IF NOT EXISTS `mygram` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `mygram`;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `creatorfans`
--

CREATE TABLE `creatorfans` (
  `Id` bigint(20) NOT NULL,
  `CreatorId` bigint(20) NOT NULL,
  `FanId` bigint(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `creators`
--

CREATE TABLE `creators` (
  `Id` bigint(20) NOT NULL,
  `Bio` text DEFAULT NULL,
  `Website` text DEFAULT NULL,
  `Username` text DEFAULT NULL,
  `Email` text DEFAULT NULL,
  `Tokens` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `fans`
--

CREATE TABLE `fans` (
  `Id` bigint(20) NOT NULL,
  `CreatorId` bigint(20) DEFAULT NULL,
  `Username` text DEFAULT NULL,
  `Email` text DEFAULT NULL,
  `Tokens` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `likedposts`
--

CREATE TABLE `likedposts` (
  `Id` bigint(20) NOT NULL,
  `FanId` bigint(20) NOT NULL,
  `PostId` bigint(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `posts`
--

CREATE TABLE `posts` (
  `Id` bigint(20) NOT NULL,
  `Image` text DEFAULT NULL,
  `Description` text DEFAULT NULL,
  `Likes` int(11) NOT NULL,
  `CreatorUsername` text DEFAULT NULL,
  `CreatorId` bigint(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `reactions`
--

CREATE TABLE `reactions` (
  `Id` int(11) NOT NULL,
  `FanId` bigint(20) NOT NULL,
  `PostId` bigint(20) NOT NULL,
  `Message` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `savedposts`
--

CREATE TABLE `savedposts` (
  `Id` bigint(20) NOT NULL,
  `FanId` bigint(20) NOT NULL,
  `PostId` bigint(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `__efmigrationshistory`
--

CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Indexen voor geëxporteerde tabellen
--

--
-- Indexen voor tabel `creatorfans`
--
ALTER TABLE `creatorfans`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_CreatorFans_CreatorId` (`CreatorId`),
  ADD KEY `IX_CreatorFans_FanId` (`FanId`);

--
-- Indexen voor tabel `creators`
--
ALTER TABLE `creators`
  ADD PRIMARY KEY (`Id`);

--
-- Indexen voor tabel `fans`
--
ALTER TABLE `fans`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Fans_CreatorId` (`CreatorId`);

--
-- Indexen voor tabel `likedposts`
--
ALTER TABLE `likedposts`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_LikedPosts_FanId` (`FanId`),
  ADD KEY `IX_LikedPosts_PostId` (`PostId`);

--
-- Indexen voor tabel `posts`
--
ALTER TABLE `posts`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Posts_CreatorId` (`CreatorId`);

--
-- Indexen voor tabel `reactions`
--
ALTER TABLE `reactions`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Reactions_FanId` (`FanId`),
  ADD KEY `IX_Reactions_PostId` (`PostId`);

--
-- Indexen voor tabel `savedposts`
--
ALTER TABLE `savedposts`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_SavedPosts_FanId` (`FanId`),
  ADD KEY `IX_SavedPosts_PostId` (`PostId`);

--
-- Indexen voor tabel `__efmigrationshistory`
--
ALTER TABLE `__efmigrationshistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- AUTO_INCREMENT voor geëxporteerde tabellen
--

--
-- AUTO_INCREMENT voor een tabel `creatorfans`
--
ALTER TABLE `creatorfans`
  MODIFY `Id` bigint(20) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT voor een tabel `creators`
--
ALTER TABLE `creators`
  MODIFY `Id` bigint(20) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT voor een tabel `fans`
--
ALTER TABLE `fans`
  MODIFY `Id` bigint(20) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT voor een tabel `likedposts`
--
ALTER TABLE `likedposts`
  MODIFY `Id` bigint(20) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT voor een tabel `posts`
--
ALTER TABLE `posts`
  MODIFY `Id` bigint(20) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT voor een tabel `reactions`
--
ALTER TABLE `reactions`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT voor een tabel `savedposts`
--
ALTER TABLE `savedposts`
  MODIFY `Id` bigint(20) NOT NULL AUTO_INCREMENT;

--
-- Beperkingen voor geëxporteerde tabellen
--

--
-- Beperkingen voor tabel `creatorfans`
--
ALTER TABLE `creatorfans`
  ADD CONSTRAINT `FK_CreatorFans_Creators_CreatorId` FOREIGN KEY (`CreatorId`) REFERENCES `creators` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_CreatorFans_Fans_FanId` FOREIGN KEY (`FanId`) REFERENCES `fans` (`Id`) ON DELETE CASCADE;

--
-- Beperkingen voor tabel `fans`
--
ALTER TABLE `fans`
  ADD CONSTRAINT `FK_Fans_Creators_CreatorId` FOREIGN KEY (`CreatorId`) REFERENCES `creators` (`Id`);

--
-- Beperkingen voor tabel `likedposts`
--
ALTER TABLE `likedposts`
  ADD CONSTRAINT `FK_LikedPosts_Fans_FanId` FOREIGN KEY (`FanId`) REFERENCES `fans` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_LikedPosts_Posts_PostId` FOREIGN KEY (`PostId`) REFERENCES `posts` (`Id`) ON DELETE CASCADE;

--
-- Beperkingen voor tabel `posts`
--
ALTER TABLE `posts`
  ADD CONSTRAINT `FK_Posts_Creators_CreatorId` FOREIGN KEY (`CreatorId`) REFERENCES `creators` (`Id`) ON DELETE CASCADE;

--
-- Beperkingen voor tabel `reactions`
--
ALTER TABLE `reactions`
  ADD CONSTRAINT `FK_Reactions_Fans_FanId` FOREIGN KEY (`FanId`) REFERENCES `fans` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Reactions_Posts_PostId` FOREIGN KEY (`PostId`) REFERENCES `posts` (`Id`) ON DELETE CASCADE;

--
-- Beperkingen voor tabel `savedposts`
--
ALTER TABLE `savedposts`
  ADD CONSTRAINT `FK_SavedPosts_Fans_FanId` FOREIGN KEY (`FanId`) REFERENCES `fans` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_SavedPosts_Posts_PostId` FOREIGN KEY (`PostId`) REFERENCES `posts` (`Id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
