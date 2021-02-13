using System;
using System.Collections.Generic;
using System.Text;
using DockerComposeExample.Domain.Models;
using Xunit;

namespace DockerComposeExample.UnitTests.Domain.Models
{
    public class TrocoItemTests
    {
        [Fact]
        public void TrocoItem_Id_NaoDeveSerUmGuidVazio()
        {
            var trocoItem = new TrocoItem(1, 10);
            Assert.NotEqual(Guid.Empty, trocoItem.Id);
        }

        [Theory(DisplayName = "Moedas Válidas")]
        [InlineData(0.01, 1)]
        [InlineData(0.05, 1)]
        [InlineData(0.10, 1)]
        [InlineData(0.25, 1)]
        [InlineData(0.50, 1)]
        [InlineData(1, 1)]
        public void TrocoItem_TipoItemTroco_DeveSerMoeda(decimal valorItem, int quantidade)
        {
            var trocoItem = new TrocoItem(valorItem, quantidade);
            Assert.Equal(TipoItemTroco.Moeda, trocoItem.TipoItemTroco);
        }

        [Theory(DisplayName = "Notas Válidas")]
        [InlineData(2, 1)]
        [InlineData(5, 1)]
        [InlineData(10, 1)]
        [InlineData(20, 1)]
        [InlineData(50, 1)]
        [InlineData(100, 1)]
        public void TrocoItem_TipoItemTroco_DeveSerNota(decimal valorItem, int quantidade)
        {
            var trocoItem = new TrocoItem(valorItem, quantidade);
            Assert.Equal(TipoItemTroco.Nota, trocoItem.TipoItemTroco);
        }
    }
}
