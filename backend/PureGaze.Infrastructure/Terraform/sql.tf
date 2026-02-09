resource "azurerm_mssql_server" "sqlserver" {
  name                         = var.sql_server_name != "" ? var.sql_server_name : "${var.project_prefix}-sql-pure-gaze"
  resource_group_name          = azurerm_resource_group.rg.name
  location                     = azurerm_resource_group.rg.location
  version                      = var.sql_server_version
  administrator_login          = var.sql_admin_login
  administrator_login_password = var.sql_admin_password
}

resource "azurerm_mssql_database" "db" {
  name                        = "${var.project_prefix}-db"
  server_id                   = azurerm_mssql_server.sqlserver.id
  collation                   = var.db_collation
  sku_name                    = var.db_sku_name
  # min_capacity                = var.db_min_capacity
  # auto_pause_delay_in_minutes = var.db_auto_pause_delay
  max_size_gb                 = var.db_max_size_gb
}
