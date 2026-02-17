resource "azurerm_resource_group" "rg" {
  location = var.location
  name     = var.resource_group_name != "" ? var.resource_group_name : "${var.project_prefix}-${var.environment}-rg"
}
