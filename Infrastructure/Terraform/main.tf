resource "azurerm_resource_group" "rg" {
  location = var.azure_location
  name     = var.azure_resource_group_name != "" ? var.azure_resource_group_name : "${var.azure_project_prefix}-${var.environment}-rg"
}
