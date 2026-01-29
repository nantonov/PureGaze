output "db_connection_string" {
  value     = "Server=tcp:${azurerm_mssql_server.sqlserver.fully_qualified_domain_name},1433;Initial Catalog=${azurerm_mssql_database.db.name};Persist Security Info=False;User ID=${var.sql_admin_login};Password=${var.sql_admin_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
  sensitive = true
}

output "web_app_url" {
  value = "https://${azurerm_linux_web_app.app.default_hostname}"
}

output "static_web_app_url" {
  value = azurerm_static_web_app.static_app.default_host_name
}

output "static_web_app_api_key" {
  value     = azurerm_static_web_app.static_app.api_key
  sensitive = true
}
