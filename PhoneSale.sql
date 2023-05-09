-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 09, 2023 at 02:06 PM
-- Server version: 10.4.28-MariaDB
-- PHP Version: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `phonesale`
--

-- --------------------------------------------------------

--
-- Table structure for table `products`
--

CREATE TABLE `products` (
  `proid` varchar(50) NOT NULL,
  `proname` varchar(50) NOT NULL,
  `proprice` int(255) NOT NULL,
  `image` varchar(500) NOT NULL,
  `inventory` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `products`
--

INSERT INTO `products` (`proid`, `proname`, `proprice`, `image`, `inventory`) VALUES
('it1', 'Samsung', 818, 'samsung.jpg', 100),
('it2', 'Iphone', 1400, 'iphone.jpg', 100),
('it3', 'ROG', 1200, 'rog.jpg', 100),
('it4', 'Xiaomi', 100, 'xiaomi.jpg', 1000),
('it5', 'Nokia', 100, 'nokia.jpg', 1000);

-- --------------------------------------------------------

--
-- Table structure for table `sellers`
--

CREATE TABLE `sellers` (
  `sellerid` varchar(64) NOT NULL,
  `sellername` varchar(64) NOT NULL,
  `sellerage` int(80) NOT NULL,
  `sellerphone` varchar(64) NOT NULL,
  `sellerpassword` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `sellers`
--

INSERT INTO `sellers` (`sellerid`, `sellername`, `sellerage`, `sellerphone`, `sellerpassword`) VALUES
('seller0', 'seller0', 20, '123456', '123456');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `userid` varchar(64) NOT NULL,
  `username` varchar(64) NOT NULL,
  `userage` int(80) NOT NULL,
  `userphone` varchar(64) NOT NULL,
  `userpassword` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`userid`, `username`, `userage`, `userphone`, `userpassword`) VALUES
('user0', 'user0', 20, '123456', '123456');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `products`
--
ALTER TABLE `products`
  ADD PRIMARY KEY (`proid`);

--
-- Indexes for table `sellers`
--
ALTER TABLE `sellers`
  ADD PRIMARY KEY (`sellerid`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`userid`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
