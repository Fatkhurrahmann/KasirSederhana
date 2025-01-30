-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Waktu pembuatan: 09 Jul 2024 pada 05.22
-- Versi server: 10.4.32-MariaDB
-- Versi PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `visual_basic`
--

-- --------------------------------------------------------

--
-- Struktur dari tabel `barang`
--

CREATE TABLE `barang` (
  `kode_barang` varchar(30) NOT NULL,
  `nama_barang` varchar(100) NOT NULL,
  `harga_barang` varchar(50) NOT NULL,
  `jumlah_barang` varchar(255) NOT NULL,
  `satuan_barang` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data untuk tabel `barang`
--

INSERT INTO `barang` (`kode_barang`, `nama_barang`, `harga_barang`, `jumlah_barang`, `satuan_barang`) VALUES
('BRG001', 'Daging Sapi Reguler', '95000', '17', 'Kg'),
('BRG002', 'Daging Sapi Premium', '120000', '14', 'Kg');

-- --------------------------------------------------------

--
-- Struktur dari tabel `detail_jual`
--

CREATE TABLE `detail_jual` (
  `no_jual` int(10) NOT NULL,
  `kode_barang` int(6) NOT NULL,
  `nama_barang` int(50) NOT NULL,
  `harga_jual` int(11) NOT NULL,
  `jumlah_jual` int(11) NOT NULL,
  `sub_total` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data untuk tabel `detail_jual`
--

INSERT INTO `detail_jual` (`no_jual`, `kode_barang`, `nama_barang`, `harga_jual`, `jumlah_jual`, `sub_total`) VALUES
(0, 0, 0, 5000, 1, 5000),
(0, 0, 0, 5000, 1, 5000),
(0, 0, 0, 5000, 1, 5000),
(0, 0, 0, 5000, 1, 5000),
(0, 0, 0, 5000, 2, 10000),
(0, 0, 0, 5000, 1, 5000),
(0, 0, 0, 5000, 1, 5000),
(0, 2, 0, 12000, 2, 24000),
(0, 0, 0, 5000, 1, 5000),
(0, 0, 0, 5000, 1, 5000),
(0, 0, 0, 5000, 1, 5000),
(0, 0, 0, 5000, 1, 5000),
(0, 0, 0, 12000, 1, 12000),
(0, 0, 0, 12000, 1, 12000),
(0, 0, 0, 12000, 1, 12000),
(0, 0, 0, 12000, 1, 12000),
(0, 0, 0, 20000, 12, 240000),
(0, 0, 0, 20000, 20, 400000),
(0, 0, 0, 20000, 16, 320000),
(0, 0, 0, 5000, 1, 5000),
(0, 0, 0, 5000, 1, 5000),
(0, 0, 0, 20000, 1, 20000),
(0, 0, 0, 5000, 1, 5000),
(0, 0, 0, 95000, 1, 95000),
(0, 0, 0, 95000, 1, 95000),
(0, 0, 0, 120000, 1, 120000),
(0, 0, 0, 95000, 1, 95000);

-- --------------------------------------------------------

--
-- Struktur dari tabel `jual`
--

CREATE TABLE `jual` (
  `no_jual` varchar(10) NOT NULL,
  `tgl_jual` date NOT NULL,
  `jam_jual` date NOT NULL,
  `item_jual` int(11) NOT NULL,
  `total_jual` int(11) NOT NULL,
  `di_bayar` int(11) NOT NULL,
  `kembali` int(11) NOT NULL,
  `id_pelanggan` varchar(6) NOT NULL,
  `id_user` varchar(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data untuk tabel `jual`
--

INSERT INTO `jual` (`no_jual`, `tgl_jual`, `jam_jual`, `item_jual`, `total_jual`, `di_bayar`, `kembali`, `id_pelanggan`, `id_user`) VALUES
('J240627001', '2024-06-27', '0000-00-00', 1, 95000, 100000, 5000, 'PLG001', 'USR001'),
('J240627002', '2024-06-27', '0000-00-00', 1, 95000, 100000, 5000, 'PLG001', 'USR001'),
('J240707003', '2024-07-07', '0000-00-00', 1, 120000, 130000, 10000, 'PLG001', 'USR001'),
('J240709004', '2024-07-09', '0000-00-00', 1, 95000, 100000, 5000, 'PLG001', 'USR001');

-- --------------------------------------------------------

--
-- Struktur dari tabel `pelanggan`
--

CREATE TABLE `pelanggan` (
  `id_pelanggan` varchar(6) NOT NULL,
  `username` varchar(30) NOT NULL,
  `alamat` varchar(30) NOT NULL,
  `no_hp` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data untuk tabel `pelanggan`
--

INSERT INTO `pelanggan` (`id_pelanggan`, `username`, `alamat`, `no_hp`) VALUES
('PLG001', 'Dzaky', 'Bogor', '081568380473'),
('PLG002', 'Harsya', 'Cikarang', '096234234322');

-- --------------------------------------------------------

--
-- Struktur dari tabel `user`
--

CREATE TABLE `user` (
  `id_user` varchar(6) NOT NULL,
  `username` varchar(30) NOT NULL,
  `password` varchar(30) NOT NULL,
  `status` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data untuk tabel `user`
--

INSERT INTO `user` (`id_user`, `username`, `password`, `status`) VALUES
('USR001', 'admin', 'admin', 'admin'),
('USR002', 'user', 'user', 'user'),
('USR004', 'Dzaky', 'admin', 'ADMIN'),
('USR005', 'usr005', '', 'ADMIN');

--
-- Indexes for dumped tables
--

--
-- Indeks untuk tabel `barang`
--
ALTER TABLE `barang`
  ADD PRIMARY KEY (`kode_barang`);

--
-- Indeks untuk tabel `jual`
--
ALTER TABLE `jual`
  ADD PRIMARY KEY (`no_jual`);

--
-- Indeks untuk tabel `pelanggan`
--
ALTER TABLE `pelanggan`
  ADD PRIMARY KEY (`id_pelanggan`);

--
-- Indeks untuk tabel `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`id_user`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
