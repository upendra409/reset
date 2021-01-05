provider "azurerm" {
    version = "2.5.0"
    features {}
}

resource "azurerm_resource_group" "tf_test" {
    name = "tfmainresourcegroup"
    location = "eastus2"
}

resource "azurerm_container_group" "tfcg_test" {
    name                        = "yogissvcgrp"
    location                    = azurerm_resource_group.tf_test.location
    resource_group_name         = azurerm_resource_group.tf_test.name
    ip_address_type             = "public"
    dns_name_label              = "yogisapis"
    os_type                     = "Linux"
    container {
        name = "initsvcapi"
        image = "upendra409/tasks.backend.initservice"
        cpu = "1"
        memory = "1"
        ports {
            port = 55001
            protocol = "TCP"
        }
    }
}