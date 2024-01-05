﻿using SupplyManagementAPI.Models;
using SupplyManagementAPI.Utilities.Enums;
using System;

namespace SupplyManagementAPI.DTOs.Accounts
{
    public class AccountDto
    {
        // Properti-properti DTO untuk objek Account
        public Guid Guid { get; set; } // GUID unik yang mengidentifikasi akun
        public StatusLevel Status { get; set; }

        // Operator eksplisit digunakan untuk mengonversi objek Account ke AccountDto
        public static explicit operator AccountDto(Account account)
        {
            return new AccountDto
            {
                Guid = account.Guid, // Mengisi properti Guid dengan nilai GUID dari objek Account yang diberikan.
                Status = account.Status,
            };
        }

        // Operator implisit digunakan untuk mengonversi objek AccountDto ke Account
        public static implicit operator Account(AccountDto accountDto)
        {
            return new Account
            {
                Guid = accountDto.Guid,
                Status = accountDto.Status,
                ModifiedDate = DateTime.Now // Mengisi properti ModifiedDate dengan tanggal dan waktu saat ini.
            };
        }
    }
}
