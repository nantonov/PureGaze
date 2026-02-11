# resource "azurerm_storage_account" "tfstate" {
#   name                            = var.storage_account_name
#   resource_group_name             = azurerm_resource_group.rg.name
#   location                        = azurerm_resource_group.rg.location
#   account_tier                    = var.st_account_tier
#   account_replication_type        = var.st_replication_type
#   allow_nested_items_to_be_public = false
# 
#   tags = {
#     environment = var.environment
#   }
# }
# 
# resource "azurerm_storage_container" "tfstate" {
#   name                  = "tfstate"
#   storage_account_id    = azurerm_storage_account.tfstate.id
#   container_access_type = "private"
# }
