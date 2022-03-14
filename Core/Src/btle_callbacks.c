#include "btle_driver.h"
#include "btle_callbacks.h"
#include "checksum.h"

#include <stdio.h>
#include <stdint.h>
#include <string.h>
#include <stdarg.h>
#include <stdlib.h>

extern HANDLE mainCharTxHandle;
extern HANDLE mainCharRxHandle;

extern uint8_t SPEED;
extern int8_t DIR;

void BTLE_CommandsHandler(uint8_t size, uint8_t *buffer){
	if(buffer[0] == 0)
		return;

	//checksum
	uint16_t c1 = (buffer[size-2]<<8)|buffer[size-1];
	uint16_t c2 = CalculateChecksum(buffer, size-2);

	if(c1 != c2){
		printf("Bad CMD checksum 0x%X 0x%X!\r\n", c1, c2);
		return;
	}

	if(buffer[0] == 0xAA){
		printf("CMD: SPEED %u DIR %d\r\n", buffer[1], (int8_t)buffer[2]);
		SPEED = buffer[1];
		DIR = (int8_t)buffer[2];
	}

	memset(buffer, 0, size);
}

void BTLE_AttributeModifiedCallback(uint16_t handle, uint8_t data_length, uint8_t *att_data){
	if(handle == mainCharRxHandle+1){
		BTLE_CommandsHandler(data_length, att_data);
	}
}
