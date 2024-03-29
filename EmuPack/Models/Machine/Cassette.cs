﻿namespace EmuPack.Models.Machine
{
    public class Cassette
    {
        public int CassetteId { get; private set; }
        public string DrugName { get; private set; }
        public int DrugQuantity { get; private set; }

        public Cassette(int cassetteId, string drugName, int drugQuantity)
        {
            CassetteId = cassetteId;
            DrugName = drugName;
            DrugQuantity = drugQuantity;
        }

        public void DecreaseDrugQuantity(int quantity)
        {
            DrugQuantity -= quantity;
        }
    }
}
