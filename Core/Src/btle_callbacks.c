#include "btle_callbacks.h"

#include <stdio.h>
#include <stdint.h>
#include <string.h>
#include <stdarg.h>
#include <stdlib.h>

void BTLE_AttributeModifiedCallback(uint16_t handle, uint8_t data_length, uint8_t *att_data){
	printf("Callback\r\n");
}
