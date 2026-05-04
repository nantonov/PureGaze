locals {
  notifications_app_settings = merge(
    {
      APPLICATIONINSIGHTS_CONNECTION_STRING = azurerm_application_insights.notifications_ai.connection_string
    },
    {
      "DatabaseOptions:ConnectionString"    = "Server=tcp:${azurerm_mssql_server.sqlserver.fully_qualified_domain_name},1433;Initial Catalog=${azurerm_mssql_database.db.name};Persist Security Info=False;User ID=${var.sql_admin_login};Password=${var.sql_admin_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
      "DatabaseOptions:MaxRetryCount"       = "3"
      "DatabaseOptions:MaxRetryDelaySecond" = "5"
    },
    {
      "Smtp:Host"     = "smtp.gmail.com"
      "Smtp:Port"     = "587"
      "Smtp:Email"    = "inna.assessment@gmail.com"
      "Smtp:Password" = var.smtp_password
      "Smtp:Enabled"  = "true"
    },
    {
      "NotificationsQueue:ConnectionString" = azurerm_storage_account.notifications_sa.primary_connection_string
    }
  )
}

resource "azurerm_storage_account" "notifications_sa" {
  name                     = var.notifications_storage_account_name
  resource_group_name      = azurerm_resource_group.rg.name
  location                 = azurerm_resource_group.rg.location
  account_tier             = "Standard"
  account_replication_type = "LRS"

  tags = {
    environment = var.environment
  }
}

resource "azurerm_service_plan" "notifications_plan" {
  name                = "${var.azure_project_prefix}-${var.environment}-notifications-asp"
  resource_group_name = azurerm_resource_group.rg.name
  location            = azurerm_resource_group.rg.location
  os_type             = "Linux"
  sku_name            = "FC1"

  tags = {
    environment = var.environment
  }
}

resource "azurerm_log_analytics_workspace" "notifications_law" {
  name                = "${var.azure_project_prefix}-${var.environment}-notifications-law"
  resource_group_name = azurerm_resource_group.rg.name
  location            = azurerm_resource_group.rg.location
  sku                 = "PerGB2018"
  retention_in_days   = 30

  tags = {
    environment = var.environment
  }
}

resource "azurerm_application_insights" "notifications_ai" {
  name                = "${var.azure_project_prefix}-${var.environment}-notifications-ai"
  resource_group_name = azurerm_resource_group.rg.name
  location            = azurerm_resource_group.rg.location
  workspace_id        = azurerm_log_analytics_workspace.notifications_law.id
  application_type    = "web"

  tags = {
    environment = var.environment
  }
}

resource "azurerm_storage_container" "notifications_deployments" {
  name                  = "deployments"
  storage_account_id    = azurerm_storage_account.notifications_sa.id
  container_access_type = "private"
}


resource "azurerm_storage_container" "tfstate" {
  name                  = "tfstate"
  storage_account_id    = azurerm_storage_account.notifications_sa.id
  container_access_type = "private"
}




resource "azurerm_function_app_flex_consumption" "notifications" {
  name                = "${var.azure_project_prefix}-${var.environment}-notifications"
  resource_group_name = azurerm_resource_group.rg.name
  location            = azurerm_resource_group.rg.location

  service_plan_id = azurerm_service_plan.notifications_plan.id

  storage_container_type      = "blobContainer"
  storage_container_endpoint  = "${azurerm_storage_account.notifications_sa.primary_blob_endpoint}${azurerm_storage_container.notifications_deployments.name}"
  storage_authentication_type = "StorageAccountConnectionString"
  storage_access_key          = azurerm_storage_account.notifications_sa.primary_access_key

  runtime_name    = "dotnet-isolated"
  runtime_version = var.dotnet_version

  app_settings = local.notifications_app_settings

  site_config {}

  tags = {
    environment = var.environment
  }
}
