Scaffold-DbContext "Server=localhost;Database=NewDataBase;Trusted_Connection=true;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Context PersonDbContext -Force
Scaffold-DbContext "Server=localhost;Database=VnStockproxx;User Id=sa;Password=abc123456;Trusted_Connection=false;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Context VnStockproxxDbContext -Force
