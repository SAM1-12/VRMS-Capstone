-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Mar 26, 2022 at 05:51 PM
-- Server version: 10.4.22-MariaDB
-- PHP Version: 8.1.2

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `registrationform`
--

-- --------------------------------------------------------

--
-- Table structure for table `visitor_archive`
--

CREATE TABLE `visitor_archive` (
  `id` int(11) NOT NULL,
  `visitor_id` varchar(30) DEFAULT NULL,
  `fname` varchar(30) DEFAULT NULL,
  `lname` varchar(30) DEFAULT NULL,
  `type` varchar(30) DEFAULT NULL,
  `plate_num` varchar(30) DEFAULT NULL,
  `time_in` varchar(30) DEFAULT NULL,
  `time_out` varchar(30) DEFAULT NULL,
  `date` varchar(30) DEFAULT NULL,
  `status` varchar(30) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `visitor_archive`
--

INSERT INTO `visitor_archive` (`id`, `visitor_id`, `fname`, `lname`, `type`, `plate_num`, `time_in`, `time_out`, `date`, `status`) VALUES
(1, '1001', 'John Kevin', 'Simangan', '2 Wheels - Bicycle', 'Bicycle', '03:04:21', 'Empty', '03-20-2022', 'Filled'),
(2, '1002', 'Sean Elli', 'Palmes', '4 Wheels - Sedan', 'ABC-1234', '03:04:21', 'Empty', '03-20-2022', 'Filled'),
(3, '1003', 'John Kevin', 'Simangan', '4 Wheels - SUV / AUV', 'ABC-1357', '03:04:21', 'Empty', '03-20-2022', 'Filled'),
(4, '1004', 'Jesus', 'Simolata', '2 Wheels - Bicycle', 'Bicycle', '03:04:21', 'Empty', '03-20-2022', 'Filled');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `visitor_archive`
--
ALTER TABLE `visitor_archive`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `visitor_id` (`visitor_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `visitor_archive`
--
ALTER TABLE `visitor_archive`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
