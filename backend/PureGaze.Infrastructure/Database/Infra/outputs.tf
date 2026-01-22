output "connection_string" {
  value     = "Server=tcp:${azurerm_mssql_server.sqlserver.fully_qualified_domain_name},1433;Database=${azurerm_mssql_database.db.name};User Id=${azurerm_mssql_server.sqlserver.administrator_login};Password=${azurerm_mssql_server.sqlserver.administrator_login_password};"
  sensitive = true
}
