variable "project_prefix" {
  type        = string
  description = "Project name prefix for resources"
  default     = "puregaze"
}

variable "environment" {
  type        = string
  description = "Environment name (e.g. dev, prod, friendname)"
  default     = "francecentral"
}

variable "sql_admin_login" {
  type        = string
  description = "Administrator login for SQL Server"
}

variable "sql_admin_password" {
  type        = string
  description = "Administrator password for SQL Server"
  sensitive   = true
}

variable "location" {
  type    = string
  default = "francecentral"
}

variable "resource_group_name" {
  type    = string
  description = "Optional override for resource group name. If empty, will use default pattern."
  default     = ""
}

variable "storage_account_name" {
  type        = string
  description = "Globally unique name for the Azure Storage Account"
}

variable "sql_server_name" {
  type        = string
  description = "Optional override for SQL server name. If empty, will use default pattern."
  default     = ""
}

variable "sql_server_version" {
  type        = string
  description = "SQL Server version"
  default     = "12.0"
}

variable "db_collation" {
  type        = string
  description = "Database collation"
  default     = "SQL_Latin1_General_CP1_CI_AS"
}

variable "db_sku_name" {
  type        = string
  description = "Database SKU name"
  default     = "GP_S_Gen5_1"
}

variable "db_min_capacity" {
  type        = number
  description = "Minimum capacity for serverless database"
  default     = 0.5
}

variable "db_auto_pause_delay" {
  type        = number
  description = "Auto pause delay in minutes"
  default     = 30
}

variable "db_max_size_gb" {
  type        = number
  description = "Maximum database size in GB"
  default     = 32
}

variable "st_account_tier" {
  type        = string
  description = "Storage account tier"
  default     = "Standard"
}

variable "st_replication_type" {
  type        = string
  description = "Storage account replication type"
  default     = "LRS"
}

variable "app_service_sku" {
  type        = string
  description = "App Service SKU (F1, B1, etc.)"
  default     = "F1"
}

variable "dotnet_version" {
  type        = string
  description = "Dotnet version for App Service"
  default     = "10.0"
}

variable "azure_service_plan_os_type" {
  type = string
  description = "OS type"
  default = "Linux"
}

variable "azure_subscription_id" {
  type = string
  description = "Azure subscription id"
}
variable "swa_location" {
  type    = string
  default = "westus2"
}

variable "swa_sku_tier" {
  type    = string
  default = "Free"
}

variable "swa_sku_size" {
  type = string 
  default = "Free"
}