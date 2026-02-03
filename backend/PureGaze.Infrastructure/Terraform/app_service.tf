resource "azurerm_service_plan" "app_plan" {
  name                = "${var.project_prefix}-${var.environment}-asp"
  resource_group_name = azurerm_resource_group.rg.name
  location            = azurerm_resource_group.rg.location
  os_type             = var.azure_service_plan_os_type
  sku_name            = var.app_service_sku

  tags = {
    environment = var.environment
  }
}

resource "azurerm_linux_web_app" "app" {
  name                = "${var.project_prefix}-${var.environment}-webapp"
  resource_group_name = azurerm_resource_group.rg.name
  location            = azurerm_resource_group.rg.location
  service_plan_id     = azurerm_service_plan.app_plan.id

  site_config {
    application_stack {
      dotnet_version = var.dotnet_version
    }
    always_on = var.app_service_sku == "F1" ? false : true
  }

  app_settings = {
    "ASPNETCORE_ENVIRONMENT" = "Development"
    "ConnectionStrings__DefaultConnection" = "Server=tcp:${azurerm_mssql_server.sqlserver.fully_qualified_domain_name},1433;Initial Catalog=${azurerm_mssql_database.db.name};Persist Security Info=False;User ID=${var.sql_admin_login};Password=${var.sql_admin_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
  }

  tags = {
    environment = var.environment
  }
}

resource "azurerm_mssql_firewall_rule" "allow_azure_services" {
  name             = "AllowAzureServices"
  server_id        = azurerm_mssql_server.sqlserver.id
  start_ip_address = "0.0.0.0"
  end_ip_address   = "0.0.0.0"
}